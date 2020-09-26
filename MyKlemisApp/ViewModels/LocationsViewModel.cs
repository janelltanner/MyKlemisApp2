using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyKlemisApp.Models;
using MyKlemisApp.Views;

namespace MyKlemisApp.ViewModels
{
    public class LocationsViewModel : BaseViewModel
    {
        public ObservableCollection<Location> Locations { get; set; }
        public Command LoadLocationsCommand { get; set; }
        public Boolean IsAdmin { get; set; }

        public LocationsViewModel()
        {
            Title = "Locations";
            Locations = new ObservableCollection<Location>();
        }

       

    }
}
