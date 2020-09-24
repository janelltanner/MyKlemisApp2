using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyKlemisApp.Services;
using MyKlemisApp.Views;
[assembly: ExportFont("Alike-Regular.ttf", Alias = "Alike")]

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
            //NavigationPage = new NavigationPage(new HomePage());
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
