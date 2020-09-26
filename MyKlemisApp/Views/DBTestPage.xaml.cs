using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Util;
using Amazon;

using MyKlemisApp.Models;
using MyKlemisApp.ViewModels;
using Amazon.DynamoDBv2.DocumentModel;

namespace MyKlemisApp.Views
{
    public partial class DBTestPage : ContentPage
    {
        DBTestViewModel viewModel;
        public DBTestPage(DBTestViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            Task food = addFoodItemsAsync();
            food.Wait();
        }
        public DBTestPage()
        {
            InitializeComponent();
            Title = "Home";
            //Task food = addFoodItemsAsync();
            //food.Wait();
            Services.TransactInterface test = new Services.TransactInterface();
            //Task t = test.authenticate();
            //t.Wait();
            test.authenticateTask();
            while (!test.isAuthorized()) { }
            test.GetInventoryItems();
        }

        private async Task addFoodItemsAsync()
        {
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                "us-east-2:d2f90bfd-19f7-4b20-ad29-09f8b19da906", // Identity pool ID
                RegionEndpoint.USEast2 // Region
            );
            var client = new Amazon.DynamoDBv2.AmazonDynamoDBClient(credentials);
            DynamoDBContext context = new DynamoDBContext(client);
            IEnumerable<ScanCondition> filters = new List<ScanCondition>() { new ScanCondition("quantity", ScanOperator.GreaterThan, 0) };
            AsyncSearch<DataModels.Food> existingFoods = context.ScanAsync<DataModels.Food>(filters);

            while(!existingFoods.IsDone)
            {
                Task<List<DataModels.Food>> task = existingFoods.GetNextSetAsync();
                task.Wait();
                List<DataModels.Food> fetched = task.Result;
                for(int i = 0; i < fetched.Count; i++)
                {
                    Label item = new Label();
                    item.Text = fetched[i].food_name + ": " + fetched[i].quantity;
                    item.WidthRequest = 150.0;
                    item.HeightRequest = 50.0;
                    layout.Children.Add(item);
                }
            }

        }
    }
}