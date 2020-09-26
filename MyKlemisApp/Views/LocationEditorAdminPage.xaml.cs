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
    public partial class LocationEditorAdminPage : ContentPage
    {
        public LocationEditorAdminPage()
        {
            InitializeComponent();
        }

        private async void SubmitEntry(Object sender, EventArgs e) {
            string[] newLocationData = new string[3];
            newLocationData[0] = locationName.Text;
            newLocationData[1] = locationAddress.Text;
            newLocationData[2] = locationHours.Text;
            await Navigation.PopAsync();
            MessagingCenter.Send<object, string[]>(this, "PassData", newLocationData);
        }
    }
}