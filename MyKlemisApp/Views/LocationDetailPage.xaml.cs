using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;


namespace MyKlemisApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationDetailPage : ContentPage
    {
        LocationDetailViewModel viewModel;
        public LocationDetailPage(LocationDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public LocationDetailPage()
        {
            InitializeComponent();
            Console.WriteLine("LOCATION DETAIL ADMIN STATUS: " + Settings.IsAdmin);
            if (Settings.IsAdmin)
            {
                ToolbarItems.Add(new ToolbarItem("Edit", "", () =>
                {
                    //logic code goes here
                }));
            }
        }

        //Handler for the 'go to inventory' button on the location page
        public void Handle_LocationInventory() {
            System.Diagnostics.Debug.WriteLine("We'll Figure this out later..");
        }

        async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}