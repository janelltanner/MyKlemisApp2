using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon;

namespace MyKlemisApp.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;
        private AnnouncementPopupPage _announcementPopup = new AnnouncementPopupPage();
        private List<Models.Announcements> announcements = new List<Models.Announcements>();
        private bool areAnnouncementsLoaded = false;
        public static string welcomeMessage = "";
        public static bool isAdminBtnVisible;
        public static bool IsAdminBtnVisible { get { return isAdminBtnVisible; } }
        public string WelcomeMessage { get { return welcomeMessage; } }
        public HomePage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public HomePage()
        {
            InitializeComponent();

            //fill announcements field
            Task announcementsTask = Task.Run(() => pullAnnouncements());
            while (areAnnouncementsLoaded == false) { }
            

            //intialize inventory cache
            Task init = Task.Run(() => TransactInterface.initialize());

            Title = "Home";
            ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
            {
                await Navigation.PushAsync(new LoginPage());

            }));
            
            this.BindingContext = this;
        }

        private async void pullAnnouncements()
        {
            CognitoAWSCredentials awsCredentials = new CognitoAWSCredentials(
               "us-east-2:d2f90bfd-19f7-4b20-ad29-09f8b19da906", // Identity pool ID
               RegionEndpoint.USEast2 // Region
           );
            var client = new Amazon.DynamoDBv2.AmazonDynamoDBClient(awsCredentials);
            DynamoDBContext context = new DynamoDBContext(client);
            IEnumerable<ScanCondition> filters = new List<ScanCondition>() { new ScanCondition("description", ScanOperator.IsNotNull) };
            AsyncSearch<Models.Announcements> credSearch = context.ScanAsync<Models.Announcements>(filters);

            while (!credSearch.IsDone)
            {
                Task<List<Models.Announcements>> task = credSearch.GetNextSetAsync();
                task.Wait();
                List<Models.Announcements> fetched = task.Result;
                for (int i = 0; i < fetched.Count; i++)
                {
                    announcements.Add(fetched[i]);
                }
            }

            areAnnouncementsLoaded = true;
        }

        async void OpenAnnouncementsForm(object sender, EventArgs e)
        {
            if (Settings.IsAdmin)
            {
                await PopupNavigation.Instance.PushAsync(_announcementPopup);
            }
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (SKPaint paint = new SKPaint())
            {
                // define the color for the shadow
                SKColor shadowColor = Color.FromHex("#a2a2a2").ToSKColor();

                paint.IsDither = true;
                paint.IsAntialias = true;
                paint.Color = shadowColor;

                // create filter for drop shadow
                paint.ImageFilter = SKImageFilter.CreateDropShadow(
                    dx: 0, dy: 0,
                    sigmaX: 40, sigmaY: 40,
                    color: shadowColor,
                    shadowMode: SKDropShadowImageFilterShadowMode.DrawShadowOnly);

                // define where I want to draw the object
                var margin = info.Width / 10;
                var shadowBounds = new SKRect(margin, -40, info.Width - margin, 10);
                canvas.DrawRoundRect(shadowBounds, 10, 10, paint);
            }
        }
    }
}