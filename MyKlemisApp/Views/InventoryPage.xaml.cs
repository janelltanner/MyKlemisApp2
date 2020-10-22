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

                //for layout purposes, everything is stuck into the grid
                Grid itemGrid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition(),
                    },
                    ColumnDefinitions =
                    {
                       new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                       new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star) },
                       new ColumnDefinition{ Width = new GridLength(0.5, GridUnitType.Star)},
                       new ColumnDefinition{ Width = new GridLength(0.5, GridUnitType.Star)},
                       new ColumnDefinition{ Width = new GridLength(0.5, GridUnitType.Star)}
                    }
                };

                Label itemName = new Label();
                
                itemName.TextColor = Color.DarkSlateGray;
                itemName.FontSize = 24;
                itemName.GestureRecognizers.Add(new TapGestureRecognizer((view) => sendToProductInfo()));
                itemName.Text = i.label;
                itemGrid.Children.Add(itemName, 1, 0);

                itemFrame.Content = layout;
                scrollLayout.Children.Add(itemFrame);

                //placeholder image -- this should be replaced by item specific images that are held in the item class
                // see Location model for example
                Image sample = new Image()
                {
                    Source = "moseBldg.jpg",
                    HeightRequest = 40,
                };
                itemGrid.Children.Add(sample);

                Console.WriteLine("LOCATION ADMIN STATUS: " + Settings.IsAdmin);
                if (!Settings.IsAdmin) {
                    Label itemQuantity = new Label()
                    {
                        Text = i.total_on_hand.ToString(),
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(5, 0, 0, 5)
                    };
                    itemGrid.Children.Add(itemQuantity, 3, 0);
                }
                else
                {
                    Button subButton = new Button
                    {
                        Text = "-",
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    itemGrid.Children.Add(subButton, 2, 0);

                    Entry itemQuantity = new Entry()
                    {
                        Text = i.total_on_hand.ToString()
                    };
                    itemGrid.Children.Add(itemQuantity, 3, 0);

                    Button addButton = new Button
                    {
                        Text = "+",
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    itemGrid.Children.Add(addButton, 4, 0);

                    var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
                    leftSwipeGesture.Swiped += OnItemSwiped;
                    layout.GestureRecognizers.Add(leftSwipeGesture);

                }

                layout.Children.Add(itemGrid);
            }

            //this isn't working 
            Console.WriteLine("LOCATION ADMIN STATUS: " + Settings.IsAdmin);
            if (!Settings.IsAdmin)
            {
                Button doneButton = new Button
                {
                    Text = "Done",
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.Center
                };

            }

        }

        public void sendToProductInfo()
        {
            //TODO (ashwin): put code to move user to product info screen in this method
        }

        //this isn't working aaaa feel free to delete -- Rebekah

        async public void OnItemSwiped(object sender, SwipedEventArgs e) {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    bool answer = await DisplayAlert("Warning", "Are you sure you want to delete this?", "Yes", "No");
                    Console.WriteLine("Answer: " + answer);

                    break;
                case SwipeDirection.Right:
                    // Handle the swipe
                    break;
                case SwipeDirection.Up:
                    // Handle the swipe
                    break;
                case SwipeDirection.Down:
                    // Handle the swipe
                    break;
            }
            
        }
    }
}
