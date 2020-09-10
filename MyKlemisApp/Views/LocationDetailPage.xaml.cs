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
    public partial class LocationDetailPage : ContentPage
    {
        public LocationDetailPage()
        {
            InitializeComponent();
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