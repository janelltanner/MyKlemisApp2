using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace MyKlemisApp.DataModels
{
    [DynamoDBTable("food_items")]
    public class Food
    {
        [DynamoDBHashKey]    // Hash key.
        public String food_name { get; set; }
        public String[] locations { get; set; }
        public int quantity { get; set; }
    }
}
