using Project1.BusinessLibrary;
using System.Collections.Generic;

namespace Project1.WebApp.Models
{
    public class ProductLocationViewModel
    {
        public Product product { get; set; }
        public List<StoreLocation> locations { get; set; }
    }
}
