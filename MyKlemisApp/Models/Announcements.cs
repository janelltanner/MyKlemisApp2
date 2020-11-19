using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyKlemisApp.Models
{
    [DynamoDBTable("Announcements")]
    public class Announcements : IComparable
    {
        public String timestamp { get; set; }
        public String description { get; set; }
        public String expiration { get; set; }
        public String poster { get; set; }
        public int CompareTo(object obj)
        {
            return -1 * timestamp.CompareTo(((Announcements)obj).timestamp);
        }
    }
}
