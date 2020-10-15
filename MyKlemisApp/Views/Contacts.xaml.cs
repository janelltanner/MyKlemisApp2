using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using MyKlemisApp.Models;
using MyKlemisApp.Services;

namespace MyKlemisApp.Views
{
    public partial class Contacts : ContentPage
    {
        public static HashSet<Admin> admins = new HashSet<Admin>();
        public static HashSet<Admin> Admins { get { return admins; } set { admins = value; } }
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
            Title = "Contacts";
        }
        protected override void OnAppearing()
        {
            AdminView.ItemsSource = admins;

        }

        public static bool contains(string username)
        {
            bool hasUser = false; 
            foreach (Admin admin in admins)
            {
                if (admin.Username.Equals(username))
                {
                    hasUser =  true; 
                }   
            }
            return hasUser;
        }
    }
}
