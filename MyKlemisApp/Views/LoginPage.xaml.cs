using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyKlemisApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyKlemisApp.Services;
using System.Collections.ObjectModel;

namespace MyKlemisApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //ObservableCollection<Admin> admins = new ObservableCollection<Admin>();
        //public ObservableCollection<Admin> Admins { get { return admins; } }
        public LoginPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

            StudentLogin.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));
            Task cred = Task.Run(() => Admin.pullCredentials());
        }
        

        async private void OnLabelClicked()
        {
            await Navigation.PushAsync(new MainPage());
            await Device.InvokeOnMainThreadAsync(() => {
                Application.Current.MainPage = new MainPage();
                Settings.IsAdmin = false;
            });

            
        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            Admin admin = new Admin(NameEntry.Text, PasswordEntry.Text);

            if (admin.CheckInformation())
            {
                await DisplayAlert("Login", "Login Success", "Okay");

                await Navigation.PushAsync(new MainPage());

                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    Settings.IsAdmin = true;
                    //admin.FullName = await DisplayPromptAsync("Welcome to MyKlemis", "What's your full name?");
                    KlemisCredentials currAdmin = Admin.GetCurrAdmin(admin);
                    if (currAdmin != null)
                    {
                        HomePage.welcomeMessage = "Welcome Back, " + currAdmin.name + "!";
                    }
                    Application.Current.MainPage = new MainPage();
                    Admin.AddToContactBook();
                    EmailPopupPage.toRecipient = currAdmin.email;
                    //HomePage.welcomeMessage = "Welcome Back, " + admin.FullName + "!";
                });
            }
            else
            {
                await DisplayAlert("Login", "Login Failed", "Okay");
            }
        }
        
    }
}
