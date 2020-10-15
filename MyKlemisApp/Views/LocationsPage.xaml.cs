using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;


namespace MyKlemisApp.Views
{
    public partial class LocationsPage : ContentPage
    {
        LocationsViewModel viewModel;
        public LocationsPage(LocationsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public LocationsPage()
        {
            InitializeComponent();
            Console.WriteLine("LOCATION ADMIN STATUS: " + Settings.IsAdmin);
            if (Settings.IsAdmin)
            {
                ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
                {
                    await Navigation.PushAsync(new LoginPage());
                }));
            }
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
