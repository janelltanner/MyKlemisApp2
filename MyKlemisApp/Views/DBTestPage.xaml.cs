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
    public partial class DBTestPage : ContentPage
    {
        DBTestViewModel viewModel;
        public DBTestPage(DBTestViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public DBTestPage()
        {
            InitializeComponent();
            Title = "Home";
        }
    }
}