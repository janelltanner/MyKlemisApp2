using System;
using System.Collections.Generic;

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
            if (Settings.IsAdmin)
            {
                ToolbarItems.Add(new ToolbarItem("Log Out", "", async () =>
                {
                    await Navigation.PushAsync(new LoginPage());
                }));
            }
        }
    }
}
