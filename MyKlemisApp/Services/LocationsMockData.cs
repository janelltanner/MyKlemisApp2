using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyKlemisApp.Models;

namespace MyKlemisApp.Services
{
    class LocationsMockData
    {
        private List<Location> locations = new List<Location>
        {
            new Location{
                Id = 1,
                Name = "Klemis Kitchen",
                Address = "Molecular Sciences And Engineering Bldg",
                Hours = "24/7",
                Img = "mouseBldg.jpg"
            },

            new Location{
                Id = 2,
                Name = "Crosland Tower",
                Address = "266 4th St NW, Atlanta, GA 30332",
                Hours = "24/7",
                Img = "croslandTower.jpg"
            }
        };

    }
}
