using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;
using MyKlemisApp.Models;

namespace MyKlemisApp.Views
{
    public partial class LocationsPage : ContentPage
    {
        LocationsViewModel viewModel;
        public IList<Location> Locations { get; private set; }


        public LocationsPage(LocationsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public LocationsPage()
        {
            InitializeComponent();
            Locations = new List<Location>();
            Locations.Add(new Location
            {
                Id = 1,
                Name = "Klemis Kitchen (Main)",
                Address = "Molecular Science and Engineering Building",
                Hours = "24/7",
                Img = "moseBldg.jpg"
            });

            Locations.Add(new Location
            {
                Id = 2,
                Name = "Library Outpost (coming soon)",
                Address = "Crossland Tower",
                Hours = "24/7",
                Img = "croslandTower.jpg"
            });

            BindingContext = this;
            Console.WriteLine("LOCATION ADMIN STATUS: " + Settings.IsAdmin);
            if (Settings.IsAdmin)
            {
                editBtn.IsVisible = true;
                ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
                {
                    await Navigation.PushAsync(new LoginPage());
                }));
            }
        }

        async void OnClickLocation(object sender, ItemTappedEventArgs e)
        {
            Location clickedLocation = e.Item as Location;
            if (clickedLocation.Name.Equals("Library Outpost (coming soon)"))
            {
                await DisplayAlert("Coming Soon", "This location is coming soon! Check back later.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new LocationDetailPage());

            }
        }

        async void OnClickEditLocations(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationsEditAdminPage());

        }
    }
}
