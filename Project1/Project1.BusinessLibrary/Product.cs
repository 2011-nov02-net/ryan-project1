﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLibrary
{
    /// <summary>
    /// product class. Contains product fields and constructor
    /// </summary>
    public class Product
    {
        public int ProductId { get; }
        public string ProductName { get; }
        public decimal ProductPrice { get; }
        public int ProductQty { get; }
        public string ProductImage { get; set; }

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
    }
}
