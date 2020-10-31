using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MyKlemisApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKlemisApp.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        private static List<KlemisCredentials> credentials = new List<KlemisCredentials>();
        private static bool areCredsLoaded = false;

        public Admin() { }

        public Admin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public static void AddToContactBook()
        {
            Contacts.admins = credentials;
           
        }

        public bool CheckInformation()
        {
            //return true;
            if(this.Username == null || this.Password == null)
            {
                return false;
            }
            while(areCredsLoaded == false) { }
            for(int i = 0; i < credentials.Count; i++) 
            {
                if (this.Username.Equals(credentials[i].username) && this.Password.Equals(credentials[i].password))
                {
                    return true;
                }
            }
            return false;
        }

        public static KlemisCredentials GetCurrAdmin(Admin ad)
        {
            foreach (KlemisCredentials kc in credentials)
            {
                if (kc.username.ToLower().Equals(ad.Username.ToLower()))
                {
                    return kc;
                } 
            }
            return null;
        }

        public static async void pullCredentials()
        {
            CognitoAWSCredentials awsCredentials = new CognitoAWSCredentials(
                "us-east-2:d2f90bfd-19f7-4b20-ad29-09f8b19da906", // Identity pool ID
                RegionEndpoint.USEast2 // Region
            );
            var client = new Amazon.DynamoDBv2.AmazonDynamoDBClient(awsCredentials);
            DynamoDBContext context = new DynamoDBContext(client);
            IEnumerable<ScanCondition> filters = new List<ScanCondition>() { new ScanCondition("username", ScanOperator.IsNotNull) };
            AsyncSearch<KlemisCredentials> credSearch = context.ScanAsync<KlemisCredentials>(filters);

            while (!credSearch.IsDone)
            {
                Task<List<KlemisCredentials>> task = credSearch.GetNextSetAsync();
                task.Wait();
                List<KlemisCredentials> fetched = task.Result;
                for (int i = 0; i < fetched.Count; i++)
                {
                    credentials.Add(fetched[i]);
                }
            }

            areCredsLoaded = true;

        }
    }
}

