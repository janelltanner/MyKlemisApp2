using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyKlemisApp
{
    public partial class EmailPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static string toRecipient = "";
        public string ToRecipient { get { return toRecipient; } }
        public Entry emailTo = new Entry();
        public EmailPopupPage()
        {
            BindingContext = this;
         
            InitializeComponent();
            //emailTo.Text = toRecipient;
        }

        async void CloseEmailForm(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        async void btnSend_Clicked(object sender, System.EventArgs e)
        {
            List<string> toAddress = new List<string>();
            toAddress.Add(emailTo.Text);
            await SendEmail(emailSubject.Text, emailBody.Text, toAddress);
        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            emailTo.Text = toRecipient;
            emailTo.HorizontalOptions = LayoutOptions.StartAndExpand;
            emailTo.WidthRequest = 320;
            emailTo.Margin = new Thickness(10, 0, 0, 0);
            ToBlock.Children.Add(emailTo);  
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }

    }
}
