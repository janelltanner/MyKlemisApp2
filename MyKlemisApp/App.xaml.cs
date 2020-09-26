using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyKlemisApp.Services;
using MyKlemisApp.Views;
[assembly: ExportFont("Alike-Regular.ttf", Alias = "Alike")]
[assembly: ExportFont("Roboto-Black.ttf", Alias = "Roboto-Black")]
[assembly: ExportFont("Roboto-BlackItaltic.ttf", Alias = "Roboto-BlackItalic")]
[assembly: ExportFont("Roboto-Bold.ttf", Alias = "Roboto-Bold")]
[assembly: ExportFont("Roboto-BoldItalic.ttf", Alias = "Robot-BoldItalic")]
[assembly: ExportFont("Roboto-Light.ttf", Alias = "Roboto-Light")]
[assembly: ExportFont("Roboto-LightItalic.ttf", Alias = "Roboto-LightItalic")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "Roboto-Medium")]
[assembly: ExportFont("Roboto-MediumItalic.ttf", Alias = "Roboto-MediumItalic")]
[assembly: ExportFont("Roboto-Regular.ttf", Alias = "Roboto-Regular")]
[assembly: ExportFont("Roboto-Thin.ttf", Alias = "Roboto-Thin")]


namespace MyKlemisApp
{
    public partial class App : Application
    {
        private NavigationPage NavigationPage;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new MainPage();
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new LoginPage();
            MainPage = new MainPage();
//             NavigationPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
