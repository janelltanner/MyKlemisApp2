using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;
using System.Linq;

namespace MyKlemisApp.Views
{
    public partial class InventorySearchResults : ContentPage
    {
        InventoryViewModel viewModel;
        List<string> randomPNGs = new List<string> { "food1.png", "food2.png", "food3.png", "food4.png", "food5.png", "food6.png", "food7.png", "food8.png" };
        Random random = new Random();
        public InventorySearchResults(InventoryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public InventorySearchResults(IEnumerable<String> itemnames,String location)
        {
            InitializeComponent();
            //Title = "Inventory";
            //Console.WriteLine("INVENTORY ADMIN STATUS: " + Settings.IsAdmin);
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
            foreach (Models.Item i in items)
            {
                if (itemnames.Contains(i.label.ToLower()) && (location=="All" || i.Location.Equals(location)))
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
                    itemName.GestureRecognizers.Add(new TapGestureRecognizer((view) => sendToProductInfo(i)));
                    itemName.Text = i.label;
                    itemGrid.Children.Add(itemName, 1, 0);

                    itemFrame.Content = layout;
                    scrollLayout.Children.Add(itemFrame);
                    int index = random.Next(randomPNGs.Count);

                    //placeholder image -- this should be replaced by item specific images that are held in the item class
                    // see Location model for example
                    Image sample = new Image()
                    {
                        //Source = "moseBldg.jpg",
                        Source = randomPNGs[index],
                        HeightRequest = 40,
                    };
                    itemGrid.Children.Add(sample);

                    Console.WriteLine("LOCATION ADMIN STATUS: " + Settings.IsAdmin);
                    if (!Settings.IsAdmin)
                    {
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

            Title = "Inventory Search";

        }

        async public void sendToProductInfo(Models.Item i)
        {
            //TODO (ashwin): put code to move user to product info screen in this method
            await Navigation.PushAsync(new ProductInfo(i));
        }

        //this isn't working aaaa feel free to delete -- Rebekah


        async public void OnItemSwiped(object sender, SwipedEventArgs e)
        {
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
