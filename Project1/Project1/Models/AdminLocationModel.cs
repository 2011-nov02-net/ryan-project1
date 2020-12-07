using Project1.BusinessLibrary;
using System.Collections.Generic;

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
