using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyKlemisApp.Views
{
    public partial class LocationsPage : ContentPage
    {
        public LocationsPage()
        {
            InitializeComponent();
        }

        async void OnClickLocation(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationDetailPage());

        }

        async void OnClickNotAvaliable(object sender, EventArgs e)
        {
            await DisplayAlert("Coming Soon", "This location is coming soon! Check back later.", "OK");
        }
    }
}
