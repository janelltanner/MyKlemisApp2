using System;
using System.Collections.Generic;
using MyKlemisApp.Models;

using Xamarin.Forms;

namespace MyKlemisApp.Views
{
    public partial class AnnouncementEntry : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public AnnouncementEntry()
        {
            InitializeComponent();
        }

        private async void NavigateButton_PostAnnouncement(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());


            //var id = (int) MenuItemType.Home;
            //await RootPage.NavigateFromMenu(id);
            //await Navigation.PushAsync(new LocationDetailPage());
        }
    }
}
