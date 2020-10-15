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
            //Title = "Home";
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