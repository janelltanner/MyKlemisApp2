using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using MyKlemisApp.Models;

using Xamarin.Forms;

namespace MyKlemisApp.Views
{
    public partial class AnnouncementEntry : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        private String name;
        private AmazonDynamoDBClient dbClient;
        public AnnouncementEntry(String posterName, AmazonDynamoDBClient client)
        {
            name = posterName;
            dbClient = client;
            InitializeComponent();
        }

        private async void NavigateButton_PostAnnouncement(object sender, EventArgs e)
        {
            //create announcement object
            Models.Announcements toPost = new Models.Announcements();
            String curr = DateTime.Now.ToString("yyyy/MM/dd*hh:mm:ss") ;
            String expire = DateTime.Now.AddMonths(1).ToString("yyyy/MM/dd*hh:mm:ss");
            toPost.timestamp = curr;
            toPost.expiration = expire;
            toPost.poster = name;
            toPost.description = descEntry.Text;

            //post announcement object to database
            DynamoDBContext context = new DynamoDBContext(dbClient);
            context.SaveAsync(toPost);

            await Navigation.PopToRootAsync();
        }
    }
}
