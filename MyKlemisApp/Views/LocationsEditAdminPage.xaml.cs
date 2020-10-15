using MyKlemisApp.Models;
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
    public partial class LocationsEditAdminPage : ContentPage
    {
        public IList<Location> Locations { get; private set; }

        public LocationsEditAdminPage()
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
        }

        async void OnClickDone(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }

        //async void OnClickDelete1(object sender, EventArgs e)
        //{
        //    locationsLayout.Children.Remove(location1);            

        //}

        //async void OnClickDelete2(object sender, EventArgs e)
        //{
        //    locationsLayout.Children.Remove(location2);

        //}

        async void OnClickEditor(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationEditorAdminPage());


        }
    }
}