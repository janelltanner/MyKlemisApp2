using System;
using System.Collections.Generic;
using System.Text;

namespace MyKlemisApp.Models
{
    class InventoryResult
    {
        public int TotalCount { get; set; }
        public List<Item> RootResults { get; set; }
    }
}
