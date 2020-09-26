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
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Home, Title="Home" },
                new HomeMenuItem {Id = MenuItemType.Locations, Title="Locations"},
                new HomeMenuItem {Id = MenuItemType.Inventory, Title="Inventory"},
                new HomeMenuItem {Id = MenuItemType.Help, Title="Help Chat"},
                new HomeMenuItem {Id = MenuItemType.DBTest, Title = "[DB Test]"}
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}