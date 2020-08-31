using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Threading.Tasks;

using MyKlemisApp.Models;
using MyKlemisApp.ViewModels;


namespace MyKlemisApp.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;
        public HomePage(HomeViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public HomePage()
        {
            InitializeComponent();
            Title = "Home";
        }
    }
}