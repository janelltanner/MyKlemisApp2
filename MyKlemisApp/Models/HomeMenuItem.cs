using System;
using System.Collections.Generic;
using System.Text;

namespace MyKlemisApp.Models
{
    public enum MenuItemType
    {
        Home,
        Locations,
        Inventory,
        Help,
        DBTest,
        AnnouncementEnter,
        LocationsAdmin,
        Contacts
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string IconSource { get; set; }
    }
}
