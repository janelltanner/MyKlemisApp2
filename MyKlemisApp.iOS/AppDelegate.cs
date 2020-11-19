using System;
using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.Util;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2.DataModel;
using Firebase.Core;
using Foundation;
using UIKit;
using UserNotifications;
using Firebase.CloudMessaging;

namespace MyKlemisApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate,  IUNUserNotificationCenterDelegate , IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        //public IMessagingDelegate MyMsg = new MessagingDelegate();
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            var loggingConfig = AWSConfigs.LoggingConfig;
            loggingConfig.LogMetrics = true;
            loggingConfig.LogResponses = ResponseLoggingOption.Always;
            loggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            loggingConfig.LogTo = LoggingOptions.SystemDiagnostics;
            AWSConfigs.AWSRegion = "us-east-2";
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            //if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            //{

            //    // For iOS 10 display notification (sent via APNS)
            //    //UNUserNotificationCenter.Current.Delegate = (IUNUserNotificationCenterDelegate)this;

            //    var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
            //    UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
            //        Console.WriteLine(granted);
            //    });
            //}
            //else
            //{
            //    // iOS 9 or before
            //    var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
            //    var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
            //    UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            //}

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10 or later
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                // For iOS 10 data message (sent via FCM)
                Messaging.SharedInstance.Delegate = this;
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }


            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            Firebase.Core.App.Configure();

            //Messaging.SharedInstance.Delegate = this;

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired till the user taps on the notification launching the application.
            // TODO: Handle data of notification

            // With swizzling disabled you must let Messaging know about the message, for Analytics
            //Messaging.SharedInstance.AppDidReceiveMessage (userInfo);

            // Print full message.
            Console.WriteLine(userInfo);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired till the user taps on the notification launching the application.
            // TODO: Handle data of notification

            // With swizzling disabled you must let Messaging know about the message, for Analytics
            //Messaging.SharedInstance.AppDidReceiveMessage (userInfo);

            // Print full message.

            Console.WriteLine(userInfo);

            completionHandler(UIBackgroundFetchResult.NewData);
        }

        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var userInfo = notification.Request.Content.UserInfo;

            // With swizzling disabled you must let Messaging know about the message, for Analytics
            //Messaging.SharedInstance.AppDidReceiveMessage (userInfo);;

            // Change this to your preferred presentation option
            completionHandler(UNNotificationPresentationOptions.None);
        }

        // Handle notification messages after display notification is tapped by the user.
        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userInfo = response.Notification.Request.Content.UserInfo;

            // Print full message.
            Console.WriteLine(userInfo);

            completionHandler();
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            var token = Messaging.SharedInstance.FcmToken ?? "";
            Console.WriteLine($"FCM token: {token}");

            // TODO: If necessary send token to application server.
            // Note: This callback is fired at each app startup and whenever a new token is generated.
        }

        public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        {
            System.Diagnostics.Debug.WriteLine( $" FCM Token: { fcmToken } ");
        }
    }
}
