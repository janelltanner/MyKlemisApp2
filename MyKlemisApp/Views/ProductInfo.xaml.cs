using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyKlemisApp.ViewModels;
using MyKlemisApp.Services;
using System.Linq;
namespace MyKlemisApp.Views
{
    public partial class ProductInfo : ContentPage
    {
        public ProductInfo()
        {
            InitializeComponent();
        }
        public ProductInfo(Models.Item i)
        {
            InitializeComponent();

            //Label lbl_name = new Label
            //{
            //    Text = "hello " + i.label
            //};
            //Content = new StackLayout
            //{
            //    Children = {
            //        lbl_name
            //    }
            //};
            Console.WriteLine("Prod Name: " + i.label);
            prodName.Text = i.label + " Product Details";
            carbs.Text = i.number;
            location.Text = i.Location;
            Title = "Item Details";
        }
    }
}
