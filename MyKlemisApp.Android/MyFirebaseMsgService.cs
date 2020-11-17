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

namespace MyKlemisApp.Droid
{
    [Service(Name = "com.xamarin.myklemisapp.MyFirebaseMsgService")]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMsgService: FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
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
            SendNotification(message.GetNotification().Body, message.Data);
        }
        private void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity)); intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }
            var pendingIntent = PendingIntent.GetActivity(this, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);
            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID).SetSmallIcon(Resource.Drawable.logo6).SetContentTitle("FCM Message").SetContentText(messageBody).SetAutoCancel(true).SetContentIntent(pendingIntent);
            var notificationManager = NotificationManagerCompat.From(this); notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}
