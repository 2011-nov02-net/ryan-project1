using Project1.BusinessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.Models
{
    public class AdminLocationModel
    {
        public List<StoreLocation> Locations { get; set; }
        public string id { get; set; }

        public AdminLocationModel()
        {
            Locations = new List<StoreLocation>();
        }
    }
}
