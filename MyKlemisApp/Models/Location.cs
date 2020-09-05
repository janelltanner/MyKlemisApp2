using System;

namespace MyKlemisApp.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Hours { get; set; }
        public string Img { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
