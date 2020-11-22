Release Notes

New Software Features
Ability to log in as an admin to access admin features
Ability to view the app as a guest/ student user
Ability to make an announcement as an admin
Ability to view announcements and the calendar
Ability to view Klemis Kitchen locations
Ability to view specific information about a Klemis Kitchen location
Ability to view items and their quantities in Klemis Kitchen’s inventory
Ability to view specific information about a particular item
Ability to search for an item by typing a keyword
Ability to search for specific items in specific locations
Ability to send out custom push notifications as an admin 
Ability to view the contact book to view contact information 

Bug Fixes
There are no bug fixes as this is the first public release. 

Know Bugs and Defects
Clicking on “View upcoming deliveries” by the calendar on the home page does nothing
Submitting new information to the locations page currently does nothing
Editing the quantities displayed on the inventory page currently does not do anything
App may freeze or slow down from time to time
UI may look different on different phone models


Installation Guide
(For a pdf version of the installation guide with pictures, please check the Files section of the Canvas Team Page.

How to Download and Build Source Code (Windows):
	
Prerequisites:
Must have Visual Studio 2019 Installed (https://visualstudio.microsoft.com/downloads/)


Download Codebase from Github Repo:
Open Visual Studio 2019. Under the “Get started” heading, select the button labeled “Clone a repository”


You will be taken to the following screen. Enter the repository URL (https://github.com/janelltanner/MyKlemisApp2) in the “Repository Location” field, then click Clone in the bottom right. Optionally, you can change the local location of the code project by altering the “Path” field. 


You will then be taken to the main development screen of Visual Studio. On the right, you will see the Solution Explorer window. You may need to select a particular solution file to open. If so, select the file called MyKlemisApp.sln.
If you have not used Visual Studio before, you may be asked to accept several software licenses.



Make sure to accept them all, as these are licenses for certain third-party software that MyKlemis relies on to run.

You should now be able to build and deploy the code. At the top of the screen, you will see a green play button:


This button builds the code and deploys a debug configuration to the device of your choice. Visual Studio allows you to run code on emulators or physical devices. For more information on Visual Studio build configurations, please see the following documentation: (https://docs.microsoft.com/en-us/visualstudio/ide/understanding-build-configurations?view=vs-2019).

How to generate .apk file for Android devices
The .apk file is an automated installer that can be used to install the application onto any Android device without deploying the app through Visual Studio. The .apk file is also what gets uploaded to the Android app marketplace once it is fully deployed. 
First, the solution file needs to be switched to a release configuration. To do this, right click on the solution file (highlighted in blue below), and select Configuration Manager.
 

Select the dropdown menu under “Active solution configuration” and select the “Release” option, then close the configuration manager. Next, right click on the solution once more and select the option “Archive All...”.

	Upon successful completion, you should see the Archive Manager window appear with two archive files, one for iOS and one for Android. Disregard the iOS archive file, as this section only pertains to the Android .apk file.
	Selecting the Android archive and clicking the Open Folder button in the bottom right will take you to the file itself. From here, you can put it on any Android device and install the app.

	Alternatively, selecting the Distribute button will allow you to sign and package the .apk for distribution services such as the Google Play Marketplace.



How to Access Amazon Web Services:
	Amazon Web Services (AWS) hosts the databases the MyKlemis uses to store and update admin information and announcements. To access the AWS console, use the link https://aws.amazon.com/console/. Select “Sign In to the Console” in the upper right corner. 


Make sure “Root user” is selected, and enter the email aguguchev@gmail.com, then hit Next. 

	You will be prompted for a password. Currently, the password is set to MyKlemis1!.  


How to Change AWS Console Credentials:
	If you would like to change the root user email address or the password, start by clicking on “klemisDB” in the upper right of the AWS console, and clicking “My Account”.


	You will be taken to the account management screen of the AWS console. From here you can change the email address and password of the account by clicking the Edit button of the Account Settings header. You can also change the primary contact information for the account by selecting the corresponding Edit button




How to Access the Application’s Database
	From the main page of the AWS console, you will see a collection of AWS services and a search bar to find more.

	
	Select “DynamoDB” from the recently visited services. If DynamoDB is not displayed, you can simply enter “DynamoDB” into the search field. This will take you to the main DynamoDB console. From here you have full access to all of DynamoDB’s features via the left sidebar. If you would like to view the data currently being stored, select the “Tables” item in the sidebar. 


	This will take you to a screen displaying the database tables currently in use by the database. You can create new tables with the “Create Table” button. To view the contents of a particular table, select the desired table name. 


	This will take you to the main table menu. From here you can access a host of preferences, metrics and alerts for the selected table. To view the data stored in the table, select the “Items” tab.


	This will show you all the data items stored in the particular table. You can edit and delete items by selecting them with the checkboxes on the left, then clicking the “Actions” button and selecting the appropriate action from the dropdown menu. You can also create new items by selecting the “Create item” button. 

	This will take you to the following screen. As a NoSQL database, DynamoDB does not follow a relational model. Each data item is stored as a document with a number of attributes. When creating items, it is imperative that those items follow a particular format. Otherwise, when the item is read by the MyKlemis application, it won’t be able to parse it and will likely generate errors. To find the particular format each table’s items must follow, please consult the Data Storage Design section of the MyKlemis Detailed Design document.


Send Out Push Notifications Through Firebase (for Android)
Note: Push Notifications cannot currently be tested with iOS because that requires enrollment in the Apple Developer Program. This has an associated cost of $99. With the free provisioning profile the services not available include: Apple Pay, Gamer Center, iCloud, In-App Purchasing, Push Notifications and Wallet

Prerequisites: 
The email address you wish to have linked to the application would need to be added as a user to the application. 
Build the app according to the Android Build instructions detailed to this document.
The device must have Google Play Services installed.

Using Firebase to Send Push Notifications
Navigate to https://firebase.google.com/. In the side menu select the option that says “Go to Console” 
Once at the console, select the project named MyKlemis 

Once at the MyKlemis project page, scroll to the section with the “Grow & engage your audience” heading and click the option that says “Cloud Messaging”   

Once on the cloud messaging page, click the button that reads “Send your first message ”

You should be directed to a “Compose notification” page with 5 stages 
In the ‘Notification’ stage 
Give the notification a title and text

In the “Target” stage
Make sure the “User segment” option is selected
Next to App, selected the android app named com.xamarin.myklemisapp (the case of the name is important)

In the “Scheduling” stage, select how you’d like the notification to be sent out. It could be scheduled for “Now”, a specific date and time, or to be a recurring notification
The next two steps are optional and can be skipped. 
Once all is filled out, click the blue review button in the bottom right had corner.  
 
After reviewing the notification in the Review Message popup that appears, click blue Publish button

Once published, the notification should be sent to the application you currently have built. 

Adding New Users to Firebase Project

Prerequisites
Must already have an account that's linked to the desired project (in this case MyKlemis)

Adding New Users 
Navigate to https://firebase.google.com/. 
Sign into Firebase with the corresponding email for the account. In the side menu select the option that says “Go to Console” 
Once at the console, select the project named MyKlemis
Locate the hamburger menu in the top left corner of the page 

 In the side navigation menu that opens, select the  located next to the label “Project Overview”
In the submenu the open up, select “Users and Permissions”

Click the blue “Add Member” button and enter the desired email address of the user you’d like to add to the project and select their desired role.
For more information on the permissions associated with each role, look here 

Troubleshooting
Visual Studio unable to build code
	Sometimes, Visual Studio’s build process can get stuck and prevent future builds from succeeding. To fix this, open Task Manager and kill all processes called “MSBuild.exe”, then try again.


Installation Guide
How to Download Source Code (Mac):

Prerequisites:
Must have Visual Studio 2019 Installed (Mac) (https://visualstudio.microsoft.com/downloads/)


Download Codebase from Github Repo:
Download the project as a zip file at the repository URL (https://github.com/janelltanner/MyKlemisApp2) or clone it with the Terminal to the directory that you want it to be. (Unzip the downloaded project)
Open Visual Studio 2019, and choose “Open” and path to the project directory



 Open “MyKlemisApp.sln” to get to the project







How to Install the Application on a Physical iOS Device

Pre-requisites:
Install Visual Studio for Mac onto your Macbook device. This application can be downloaded from this link: https://visualstudio.microsoft.com/vs/mac/ 
Create an Apple ID if you don’t currently have one using this link: https://appleid.apple.com/ 
Install Xcode on your MacBook
Navigate to your MacBook’s AppStore
Sign in with your Apple ID
Search for Xcode in the AppStore 
Click “install” 
Create an Apple Xcode Free Provisioning Profile (Detailed in the “Create an Xcode Free Provisioning Profile” Section)

Build Instructions for a Physical iOS Device: 
Open the provided “MyKlemisApp.sln” file in Visual Studio for Mac.
You can do this by either double-clicking the file in the Mac Finder or Open Visual Studio on your MacBook and in the window click the “Open” button in the right side panel and locate the where the file is stored on your device. 

Make sure your iOS device is plugged in to your MacBook
In the project solution bar, double click the project solution named  “MyKlemisApp.iOS”.

A window should open named “Project Options”. In the side-menu, select the option named “iOS Bundle Signing”
In the field labeled “Signing Identity” select the Developer email you used in creating the Xcode Provisioning Profile in the prerequisites section of this document. 
In the field labeled “Provisioning Profile” there should be an option to select the provisioning profile that was automatically created during the Xcode Provisioning Profile in the prerequisites section. The naming the should match what’s displayed in the image below

To build to app your physical iOS device, at the top of the Visual Studio window, located the bar that allows you to select the project build configurations 

Next to the first , select the option that reads MyKlemisApp.iOS. 
Next to the second , select the option that reads Debug
Next to the  icon, select the name given you your iOS device

Once you click the play button, ▶, this should begin to the process of installing the application  onto your physical device
Once you click on the app on your device, you should receive a message similar to the one pictured below. 

In order to resolve this issue, go to your device Settings, go to General and locate the field labeled Device Management. 

Select the Developer App and then select the option in blue that says “Trust ‘Apple Development: ….’”

You should now be able to use the application on your iOS device



Create an Xcode Free Provisioning Profile
Open Xcode and navigate to Xcode > Preferences.

In the window that opens, select Accounts. Use the + button to add your existing Apple ID. It should look similar to the screenshot below:

Close Xcode preferences.
Plugin the iOS device to which you'd like to deploy your app.
In Xcode, create a new project. Choose File > New > Project and select Single View App or App depending on what is available to you
In the new project dialog, set Team to the Apple ID that you just added. In the drop-down list, it should look similar to Your Name (Personal Team)

Once the new project has been created, choose an Xcode build scheme that targets your iOS device (rather than a simulator).

Open your app's project settings by selecting its top-level node in Xcode's Project Navigator.
Under General > Identity, make sure that the Bundle Identifier exactly matches your Xamarin.iOS app's bundle identifier found in the Info.plist file. (This identifier for the MyKlemis app is com.xamarin.MyKlemisApp)

Under Deployment Info, ensure that the deployment target matches or is lower than the version of iOS installed on your connected iOS device.
Under Signing, select Automatically manage signing and select your team from the drop-down list:

Xcode will automatically generate a provisioning profile and signing identity for you. You can view this by clicking on the information icon next to provisioning profile


