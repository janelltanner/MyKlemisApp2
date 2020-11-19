using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyKlemisApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class AllAnnouncementsPage : ContentPage
    {
        private List<Models.Announcements> announcements = new List<Models.Announcements>();
        public AllAnnouncementsPage()
        {
            InitializeComponent();
        }
        public AllAnnouncementsPage(List<Models.Announcements> anno)
        {
            InitializeComponent();
            announcements = anno;

            foreach(Models.Announcements ann in announcements)
            {
                StackLayout annLayout = new StackLayout();
                Frame annFrame = new Frame();
                annFrame.BackgroundColor = Color.White;
                annFrame.HasShadow = false;
                annLayout.Orientation = StackOrientation.Horizontal;

                Label annText = new Label();
                Label annDate = new Label();

                annText.TextColor = Color.DarkSlateGray;
                annText.FontFamily = "Roboto-Regular";
                annText.FontSize = 20;
                annText.Text = ann.description;

                annLayout.Children.Add(annText);

                /*                annDate.TextColor = Color.DarkSlateGray;
                                annDate.FontFamily = "Roboto-Regular";
                                annDate.FontSize = 13;
                                annDate.Text = ann.timestamp;
                                annDate.HorizontalOptions = LayoutOptions.EndAndExpand;
                                annDate.LineBreakMode = LineBreakMode.NoWrap;
                                annLayout.Children.Add(annDate);*/

                annFrame.Content = annLayout;
                scrollLayout.Children.Add(annFrame);
            }
        }
    }
}