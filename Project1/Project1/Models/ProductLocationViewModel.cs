using Project1.BusinessLibrary;
using System.Collections.Generic;

namespace Project1.WebApp.Models
{
    public class ProductLocationViewModel
    {
        public List<StoreLocation> locations { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQty { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice { get; set; }

        public int LocationId { get; set; }
    }
}
