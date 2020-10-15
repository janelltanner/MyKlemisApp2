using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;


namespace MyKlemisApp.Views
{
    public partial class InventoryPage : ContentPage
    {
        InventoryViewModel viewModel;
        public InventoryPage(InventoryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public InventoryPage()
        {
            InitializeComponent();
            //Title = "Inventory";
            Console.WriteLine("INVENTORY ADMIN STATUS: " + Settings.IsAdmin);
            if (Settings.IsAdmin)
            {
                ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
                {
                    await Navigation.PushAsync(new LoginPage());
                }));
            }

        }
    }
}
