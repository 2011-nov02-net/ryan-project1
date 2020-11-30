using Project1.BusinessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.Models
{
    public class ProductLocationViewModel
    {
        public Product product { get; set; }
        public List<StoreLocation> locations { get; set; }
    }
}
