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
        public ObservableCollection<ItemB> Calculos { get; }

        public GeocodingSensor()
        {
            InitializeComponent();
            Calculos = new ObservableCollection<ItemB>();
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
            try
            {
                string address = txtAddress.Text;
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
                        }
                    }
                } else
                {
                    cleanInputs();
                    await DisplayAlert("Faild", "Write a location", "OK");
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                cleanInputs();
                await DisplayAlert("Warning!", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                cleanInputs();
                await DisplayAlert("Warning!", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                cleanInputs();
                await DisplayAlert("Warning!", ex.Message, "OK");
            }
        }
    }


    public class ItemB
    {
        public string DateInfo { get; set; }
        public string NetworkAccess { get; set; }
        public string ConnectionProfiles { get; set; }
    }
}