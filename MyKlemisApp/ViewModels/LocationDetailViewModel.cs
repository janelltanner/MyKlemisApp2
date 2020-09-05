using System;

using MyKlemisApp.Models;

namespace MyKlemisApp.ViewModels
{
    public class LocationDetailViewModel : BaseViewModel
    {
        public Location Location { get; set; }
        public LocationDetailViewModel(Location location = null)
        {
            Title = location?.Name;
            Location = location;
        }
    }
}
