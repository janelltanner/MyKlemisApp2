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

            //add inventory items
            List<Models.Item> items = Models.InventoryCache.getItems();
            items.Sort();
            foreach(Models.Item i in items)
            {
                Frame itemFrame = new Frame();
                StackLayout layout = new StackLayout();
                itemFrame.BackgroundColor = Color.LightBlue;
                layout.Orientation = StackOrientation.Horizontal;
                Label itemName = new Label();
                itemName.TextColor = Color.DarkSlateGray;
                itemName.FontSize = 24;
                itemName.GestureRecognizers.Add(new TapGestureRecognizer((view) => sendToProductInfo()));
                itemName.Text = i.label;
                layout.Children.Add(itemName);
                itemFrame.Content = layout;
                scrollLayout.Children.Add(itemFrame);
            }

        }

        public void sendToProductInfo()
        {
            //TODO (ashwin): put code to move user to product info screen in this method
        }
    }
}
