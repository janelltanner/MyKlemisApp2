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
        public LocationsEditAdminPage()
        {
            InitializeComponent();
        }

        async void OnClickDone(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }

        async void OnClickDelete1(object sender, EventArgs e)
        {
            locationsLayout.Children.Remove(location1);            

        }

        async void OnClickDelete2(object sender, EventArgs e)
        {
            locationsLayout.Children.Remove(location2);

        }

        async void OnClickEditor(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationEditorAdminPage());


        }
    }
}