using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Threading.Tasks;

using MyKlemisApp.Models;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;

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
            Settings.IsAdmin = true;
            //isAdmin = viewModel.IsAdmin;
            Console.WriteLine("HOME ADMIN STATUS: " + Settings.IsAdmin);
            if (Settings.IsAdmin)
            {
                ToolbarItems.Add(new ToolbarItem("Edit", "", () =>
                {
                    //logic code goes here
                }));
            }
            //BC = getBackgroundColor(Settings.IsAdmin);
        }
        //public Color BC
        //{
        //    set
        //    {
        //        BC = value;
        //    }
        //    get
        //    {
        //        return BC;
        //    }
        //} 
        //public Color getBackgroundColor (bool adminStatus)
        //{
        //    switch (adminStatus)
        //    {
        //        case true:
        //            return Color.FromHex("#1E4471");
        //        case false:
        //            return Color.FromHex("#5BBB93");
        //        default:
        //            return Color.FromHex("#5BBB93");

        //    }
        //}
    }
}