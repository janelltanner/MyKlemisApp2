using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;
using System.Linq;

namespace MyKlemisApp.Views
{
    public partial class InventoryPage : ContentPage
    {
        InventoryViewModel viewModel;
        List<string> randomPNGs = new List<string> { "food1.png", "food2.png", "food3.png", "food4.png", "food5.png", "food6.png", "food7.png", "food8.png" };
        Random random = new Random();
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
            ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
            {
                await Navigation.PushAsync(new LoginPage());
            }));

            //add inventory items
            List<Models.Item> items = Models.InventoryCache.getItems();
            List<String> locations = getLocations(items);
            LocationPicker.Items.Add("All");
            LocationPicker.Items.Add("Demo");
            foreach (String s in locations)
            {
                LocationPicker.Items.Add(s);
            }
            LocationPicker.SelectedIndex = 0;
            foreach(Models.Item i in items)
            {
                Frame itemFrame = new Frame();
                StackLayout layout = new StackLayout();
                itemFrame.BackgroundColor = Color.White;
                itemFrame.HasShadow = false;
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
                itemName.FontFamily = "Roboto-Regular";
                itemName.FontSize = 20;
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

                //Console.WriteLine("LOCATION ADMIN STATUS: " + Settings.IsAdmin);
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

        async public void sendToProductInfo(Models.Item i)
        {
            //TODO (ashwin): put code to move user to product info screen in this method
            await Navigation.PushAsync(new ProductInfo(i));
        }

        public List<String> getLocations(List<Models.Item> items)
        {
            List<String> locations = new List<String>();
            foreach (Models.Item i in items)
            {
                if (!locations.Contains(i.Location))
                {
                    locations.Add(i.Location);
                }
            }
            return locations;

        }

        //this isn't working aaaa feel free to delete -- Rebekah

        async void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            List<Models.Item> items = Models.InventoryCache.getItems();
            List<String> itemnames = new List<String>();
            foreach (Models.Item i in items)
            {
                itemnames.Add(i.label);
            }

            IEnumerable<String> itemsSearched = itemnames.Where(c => c.Contains(InventorySearch.Text));
            await Navigation.PushAsync(new InventorySearchResults(itemsSearched, LocationPicker.Items[LocationPicker.SelectedIndex]));


        }

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
