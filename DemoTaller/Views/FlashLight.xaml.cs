using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoTaller.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlashLight : ContentPage
    {
        public ObservableCollection<ItemA> Log { get; }

        public FlashLight()
        {
            InitializeComponent();
            Log = new ObservableCollection<ItemA>();
        }

        private bool FlashLightStatus = false;

        private async void FlashLightCheckButton_Clicked(object sender, System.EventArgs e)
        {
            string statusText = "FlashLight Status: ";
            try
            {
                FlashLightStatus = !FlashLightStatus;
                if (FlashLightStatus)
                {
                    // Turn On
                    await Flashlight.TurnOnAsync();
                    statusText += "On";
                }
                else 
                { 
                    // Turn Off
                    await Flashlight.TurnOffAsync();
                    statusText += "Off";
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Warning!", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Warning!", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning!", ex.Message, "OK");
            }
            var item = new ItemA
            {
                DateInfo = System.DateTime.Now.ToString(),
                State = statusText,
            };
            Log.Insert(0, item);
            ListViewCheck.ItemsSource = Log;
        }
    }

    public class ItemA
    {
        public string DateInfo { get; set; }
        public string State { get; set; }
    }
}