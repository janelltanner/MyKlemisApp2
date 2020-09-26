using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyKlemisApp.Models;
using Newtonsoft.Json;
using System.IO;

namespace MyKlemisApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case (int)MenuItemType.Locations:
                        MenuPages.Add(id, new NavigationPage(new LocationsPage()));
                        break;
                    case (int)MenuItemType.Inventory:
                        MenuPages.Add(id, new NavigationPage(new InventoryPage()));
                        break;
                    case (int)MenuItemType.Help:
                        MenuPages.Add(id, new NavigationPage(new HelpPage()));
                        break;
                    case (int)MenuItemType.DBTest:
                        MenuPages.Add(id, new NavigationPage(new DBTestPage()));
                        break;
                    case (int)MenuItemType.LocationsAdmin:
                        MenuPages.Add(id, new NavigationPage(new LocationsAdminPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}