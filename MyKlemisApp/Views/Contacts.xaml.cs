using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using MyKlemisApp.Models;
using MyKlemisApp.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace MyKlemisApp.Views
{
    public partial class Contacts : ContentPage
    {
        private EmailPopupPage _emailPopup;
        private Admin admin = new Admin();
        //public static List<KlemisCredentials> admins = new List<KlemisCredentials>();
        //public static List<KlemisCredentials> Admins { get { return admins; } set { admins = value; } }
        public Contacts()
        {
            if (Settings.IsAdmin)
            {
                ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
                {
                    await Navigation.PushAsync(new LoginPage());

                    //await Device.InvokeOnMainThreadAsync(() => {
                    //    Application.Current.MainPage = new LoginPage();
                    //});
                }));
            }
            InitializeComponent();
            this.BindingContext = this;
            //ContactAdmins.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));
            Title = "Contacts";
            _emailPopup = new EmailPopupPage();
        }
        protected override void OnAppearing()
        {
            AdminView.ItemsSource = admin.getCredentials();

        }

        void OnItemSelected(object sender, EventArgs e)
        {
            KlemisCredentials selectedItem = (KlemisCredentials)AdminView.SelectedItem;
            EmailPopupPage.toRecipient = selectedItem.email;

        }

        async void OpenEmailForm(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(_emailPopup);
            //await Navigation.PushAsync(new MainPage());
            //await Device.InvokeOnMainThreadAsync(() => {
            //    Application.Current.MainPage = new MainPage();
            //    Settings.IsAdmin = false;
            //});
            //ContactAdmins;
        }
    }
}
