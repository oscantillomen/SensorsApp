using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoTaller.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeocodingSensor : ContentPage
    {
        public ObservableCollection<ItemB> Log { get; }

        public GeocodingSensor()
        {
            InitializeComponent();
            Log = new ObservableCollection<ItemB>();
        }

        public void cleanInputs()
        {
            LabelAdminArea.Text = "";
            LabelCountryName.Text = "";
            LabelCountryCode.Text = "";
            LabelLocality.Text = "";
            LabelSubAdminArea.Text = "";
            LabelSublocality.Text = "";
            LabelPostalcode.Text = "";
            LabelThoroughfare.Text = "";
            LabelSubThoroughfare.Text = "";
        }

        private async void GeocodingButton_Clicked(object sender, EventArgs e)
        {
            string stateText = "Error";
            string address = txtAddress.Text;
            try
            {
                if (!string.IsNullOrEmpty(address))
                {
                    var locations = await Geocoding.GetLocationsAsync(address);

                    var location = locations?.FirstOrDefault();

                    if (location != null)
                    {
                        var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                        var placemark = placemarks?.FirstOrDefault();
                        if (placemark != null)
                        {
                            LabelAdminArea.Text = "Admin Area: " + placemark.AdminArea;
                            LabelCountryName.Text = "Country Name:" + placemark.CountryName;
                            LabelCountryCode.Text = "Country Code:" + placemark.CountryCode;
                            LabelLocality.Text = "Locality:" + placemark.Locality;
                            LabelSubAdminArea.Text = "SubAdmin Area:" + placemark.SubAdminArea;
                            LabelSublocality.Text = "SubLocality:" + placemark.SubLocality;
                            LabelPostalcode.Text = "PostalCode:" + placemark.PostalCode;
                            LabelThoroughfare.Text = "Thoroughfare:" + placemark.Thoroughfare;
                            LabelSubThoroughfare.Text = "SubThoroughfare:" + placemark.SubThoroughfare;
                            stateText = "Success";
                        }
                    }
                } else
                {
                    cleanInputs();
                    await DisplayAlert("Warning!", "Write a location", "OK");
                    address = "Empty Input";
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                cleanInputs();
                await DisplayAlert("Warning!", fnsEx.Message, "OK");
                address = fnsEx.Message;
            }
            catch (PermissionException pEx)
            {
                cleanInputs();
                await DisplayAlert("Warning!", pEx.Message, "OK");
                address = pEx.Message;
            }
            catch (Exception ex)
            {
                cleanInputs();
                await DisplayAlert("Warning!", ex.Message, "OK");
                address = ex.Message;
            }

            var item = new ItemB
            {
                Search = address,
                State = stateText,
            };
            Log.Insert(0, item);
            ListViewCheck.ItemsSource = Log;
        }
    }


    public class ItemB
    {
        public string Search { get; set; }
        public string State { get; set; }
    }
}