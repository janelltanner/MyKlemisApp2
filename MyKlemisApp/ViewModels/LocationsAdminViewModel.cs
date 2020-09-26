using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyKlemisApp.Models;
using MyKlemisApp.Views;

namespace MyKlemisApp.ViewModels
{
    public class LocationsAdminViewModel : BaseViewModel
    {
        public ObservableCollection<Location> Locations { get; set; }
        public Command LoadLocationsCommand { get; set; }

        public LocationsAdminViewModel()
        {
            Title = "Locations Admin";

        }
    }
}
