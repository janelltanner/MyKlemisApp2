using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace MyKlemisApp.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;
        public static string welcomeMessage = "";
        public string WelcomeMessage { get { return welcomeMessage; } }
        public HomePage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
        public HomePage()
        {
            InitializeComponent();
<<<<<<< HEAD
            //Title = "Home";
=======
            //intialize inventory cache
            Task init = Task.Run(() => TransactInterface.initialize());

            Title = "Home";
            Settings.IsAdmin = true;
            //isAdmin = viewModel.IsAdmin;
            Console.WriteLine("HOME ADMIN STATUS: " + Settings.IsAdmin);
>>>>>>> 8fbf6b3f7b3a0438c0a6052003a521a86ad4230f
            if (Settings.IsAdmin)
            {
                //HomeBackground.BackgroundColor = Color.White;
                ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
                {
                    await Navigation.PushAsync(new LoginPage());
                    
                    //await Device.InvokeOnMainThreadAsync(() => {
                    //    Application.Current.MainPage = new LoginPage();
                    //});
                }));
            } else
            {
                welcomeMessage = "MyKlemis";
                //HomeBackground.BackgroundColor = Color.FromHex("#5BBB93");
            }
            this.BindingContext = this;
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