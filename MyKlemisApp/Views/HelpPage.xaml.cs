using System;
using System.Collections.Generic;

using Xamarin.Essentials;

using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;

namespace MyKlemisApp.Views
{
    public partial class HelpPage : ContentPage
    {
        HelpViewModel viewModel;
        public HelpPage(HelpViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        public HelpPage()
        {
            InitializeComponent();
            Title = "Help";
            Console.WriteLine("HELP ADMIN STATUS: " + Settings.IsAdmin);
            ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
            {
                await Navigation.PushAsync(new LoginPage());
            }));
        }

        private async void OnClickedEmail(System.Object sender, System.EventArgs e)
        {
            //var email = new EmailMessage(EntrySubject.Text, "", EntryEmail.Text);

            //await Email.ComposeAsync(email);
            List<string> list = new List<string>();
            list.Add("klemiskitchen@support.com");

            try
            {
                var message = new EmailMessage
                {
                    Subject = "",
                    Body = "",
                    To = list
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
            //await Email.ComposeAsync("", "", "");
        }
    }
}
