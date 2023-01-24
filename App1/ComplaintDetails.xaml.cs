using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;


namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComplaintDetails : ContentPage
    {
        CancellationTokenSource cts;
        public ComplaintDetails()
        {
            InitializeComponent();
        }

        private async void UploadImage(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();
            var stream = await result.OpenReadAsync();
            selectedImage.Source = ImageSource.FromStream(() => stream);

        }

        private async void selectLocation(object sender, EventArgs e)
        {
            try
            {
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                if (location != null)
                {
                    Position position = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
                    Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map(mapSpan);

                    Geocoder geocoder = new Geocoder();
                    var address = await geocoder.GetAddressesForPositionAsync(position);
                    labelLocation.Text = address.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
               var s= ex.Message;
            }
        }

        private async void SubmitComplaint(object sender, EventArgs e)
        {
            //await App.Current.MainPage.DisplayAlert("Success", "Your Complaint has been successfully submitted", "OK");
            await ShowMessage("Success", "Your Complaint has been successfully submitted", "Ok", async () => {
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            });
        }
        public async Task ShowMessage(string message,
        string title,
        string buttonText,
        Action afterHideCallback)
        {
            await DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }
    }
}