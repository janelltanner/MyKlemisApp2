using Newtonsoft.Json;
using System;

namespace MyKlemisApp.Models
{
    public class Item : IComparable
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        public string Text { get; set; }

        [JsonProperty("class_name")]
        public string Location { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
        public string number { get; set; }
        public string label { get; set; }
        public bool enabled { get; set; }
        public bool outofstock { get; set; }
        public int total_on_hand { get; set; }

        public int CompareTo(object obj)
        {
            return label.CompareTo(((Item)obj).label);
        }
    }
}