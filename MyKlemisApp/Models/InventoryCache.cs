using System;
using System.Collections.Generic;
using System.Text;

namespace MyKlemisApp.Models
{
    class InventoryCache
    {
        private static List<Item> items = new List<Item>();
        public static void setItems(List<Item> setItems)
        {
            items = setItems;
        }

        public static List<Item>.Enumerator getItemsEnum()
        {
            return items.GetEnumerator();
        }

        public static int getCount()
        {
            return items.Count;
        }
    }
}
