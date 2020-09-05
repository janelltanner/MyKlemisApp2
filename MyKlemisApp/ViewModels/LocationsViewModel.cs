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

        public LocationsViewModel()
        {
            Title = "Locations";
            Locations = new ObservableCollection<Location>();

            //LoadLocationsCommand = new Command(async () => await ExecuteLoadLocationsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Location>(this, "AddItem", async (obj, item) =>
            //{
            //    var newLocation = location as Location;
            //    Locations.Add(newLocation);
            //    await DataStore.AddItemAsync(newLocation);
            //});
        }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    IsBusy = true;

        //    try
        //    {
        //        Locations.Clear();
        //        var locations = await DataStore.GetItemsAsync(true);
        //        foreach (var location in locations)
        //        {
        //            Locations.Add(location);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}
