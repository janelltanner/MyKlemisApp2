using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyKlemisApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyKlemisApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            Admin admin = new Admin(NameEntry.Text, PasswordEntry.Text);

            if (admin.CheckInformation())
            {
                await DisplayAlert("Login", "Login Success", "Okay");

                await Navigation.PushAsync(new MainPage());

                await Device.InvokeOnMainThreadAsync(() => {
                    Application.Current.MainPage = new MainPage();
                });

                //await Navigation.PushAsync(new NavigationPage(new MainPage()));

                //await Navigation.PushAsync(new NavigationPage(new HomePage()));
            }
            else
            {
                await DisplayAlert("Login", "Login Failed", "Okay");
            }
        }
    }
}
