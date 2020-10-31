using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyKlemisApp.Models;
using Newtonsoft.Json;
using System.IO;
using MyKlemisApp.Services;

namespace MyKlemisApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class StudentMainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public StudentMainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);


        }
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new StudentHomePage()));
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
