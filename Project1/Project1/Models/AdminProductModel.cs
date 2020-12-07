using Project1.BusinessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.Models
{
    public class AdminProductModel
    {
        public List<Product> Products { get; set; }
        public string id { get; set; }

        public AdminProductModel()
        {
            Products = new List<Product>();
        }
    }
}
