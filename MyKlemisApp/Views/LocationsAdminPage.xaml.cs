using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyKlemisApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationsAdminPage : ContentPage
    {
        public LocationsAdminPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<object, string[]>(this, "PassData", (sender, args) =>
            {

                loc1Name.Text = args[0];
            });
        }

        async void OnClickLocation(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationDetailPage());

        }
        async void OnClickNotAvaliable(object sender, EventArgs e)
        {
            await DisplayAlert("Coming Soon", "This location is coming soon! Check back later.", "OK");
        }

        async void OnClickEditLocations(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationsEditAdminPage());

        }


    }
}