using System.ComponentModel.DataAnnotations;

namespace Project1.BusinessLibrary
{
    /// <summary>
    /// product class. Contains product fields and constructor
    /// </summary>
    public class Product
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; }

        [Display(Name = "Product Name")]
        public string ProductName { get; }

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; }

        [Display(Name = "Product Qty")]
        public int ProductQty { get; set; }

        [Display(Name = "Image URL")]
        public string ProductImage { get; set; }
        public int LocationId { get; set; }

        public Product(int id, string name, decimal price, int qty, string url)
        {
            ProductId = id;
            ProductName = name;
            ProductPrice = price;
            ProductQty = qty;
            ProductImage = url;
        }

        public Product(int id, string name, decimal price, string url)
        {
            ProductId = id;
            ProductName = name;
            ProductPrice = price;
            ProductImage = url;
        }

        public Product(int id, string name, decimal price, int qty, int location)
        {
            ProductId = id;
            ProductName = name;
            ProductPrice = price;
            ProductQty = qty;
            LocationId = location;
        }
    }
}

