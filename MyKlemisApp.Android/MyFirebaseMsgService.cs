using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using Xamarin.Forms.Internals;
using Firebase.Messaging;
using MyKlemisApp.Droid;
using System.Collections.Generic;
using Android.Content;
using AndroidX.Core.App;
using System.Text.RegularExpressions;
using Android.OS;
using System.Drawing;
using Android.Graphics;

namespace MyKlemisApp.Droid
{
    [Service(Name = "com.xamarin.myklemisapp.MyFirebaseMsgService")]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMsgService: FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public const string PRIMARY_CHANNEL = "default";
        public override void OnNewToken(string s)
        {
            //var refreshedToken = FirebaseInstanceId.Instance.Token;
            //Android.Util.Log.Debug(TAG, "New token: " + refreshedToken);
            base.OnNewToken(s);
            Android.Util.Log.Debug(TAG, "New token: " + s);
            SendRegistrationToServer(s);
        }
        void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed. 
        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            SendNotifications(message);
        }
    
        public void SendNotifications(RemoteMessage message)
        {
            try
            {
                NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
                var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
                int id = new Random(seed).Next(000000000, 999999999);
                var push = new Intent();
                var fullScreenPendingIntent = PendingIntent.GetActivity(this, 0,
               push, PendingIntentFlags.CancelCurrent);
                NotificationCompat.Builder notification;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    var chan1 = new NotificationChannel(PRIMARY_CHANNEL,
                     new Java.Lang.String("Primary"), NotificationImportance.High);
                    chan1.LightColor = Android.Graphics.Color.Green;
                    manager.CreateNotificationChannel(chan1);
                    notification = new NotificationCompat.Builder(this, PRIMARY_CHANNEL);
                }
                else
                {
                    notification = new NotificationCompat.Builder(this);
                }
                notification.SetContentIntent(fullScreenPendingIntent)
                         .SetContentTitle(message.GetNotification().Title)
                         .SetContentText(message.GetNotification().Body)
                         .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.notifLogo2))
                         .SetSmallIcon(Resource.Drawable.notifLogo2)
                         .SetStyle((new NotificationCompat.BigTextStyle()))
                         .SetPriority(NotificationCompat.PriorityHigh)
                         .SetColor(0x9c6114)
                         .SetAutoCancel(true);
                manager.Notify(id, notification.Build());
            }
            catch (Exception ex)
            {
            }
        }
    }
}
