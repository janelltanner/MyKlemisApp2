using MyKlemisApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyKlemisApp.Services;

namespace MyKlemisApp.Views
{
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        StudentMainPage RootPage2 { get => Application.Current.MainPage as StudentMainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

           if (Settings.IsAdmin)
            {
                menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Home, Title="Home", IconSource="" },
                    new HomeMenuItem {Id = MenuItemType.Locations, Title="Locations", IconSource=""},
                    new HomeMenuItem {Id = MenuItemType.Inventory, Title="Inventory", IconSource="" },
                    new HomeMenuItem {Id = MenuItemType.Help, Title="Help Chat", IconSource=""},
                    //new HomeMenuItem {Id = MenuItemType.DBTest, Title = "[DB Test]", IconSource = ""},
                    //new HomeMenuItem {Id = MenuItemType.AnnouncementEnter, Title = "Announcement Entry", IconSource = ""},
                    new HomeMenuItem {Id = MenuItemType.Contacts, Title="Contacts", IconSource=""}
                };
            } else
            {
                menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Home, Title="Home", IconSource="" },
                    new HomeMenuItem {Id = MenuItemType.Locations, Title="Locations", IconSource=""},
                    new HomeMenuItem {Id = MenuItemType.Inventory, Title="Inventory", IconSource="" },
                    new HomeMenuItem {Id = MenuItemType.Help, Title="Help Chat", IconSource=""},
                    //new HomeMenuItem {Id = MenuItemType.DBTest, Title = "[DB Test]", IconSource = ""}
                };
            }

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                if (Settings.IsAdmin)
                {
                    await RootPage.NavigateFromMenu(id);
                } else
                {
                    await RootPage2.NavigateFromMenu(id);
                }
           
            };
        }
    }
}