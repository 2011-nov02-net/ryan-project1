using System;
using System.Collections.Generic;
using System.Text;
using Project1.BusinessLibrary.Interfaces;

namespace Project1.BusinessLibrary
{
    /// <summary>
    /// OrderProduct class. Contains orderproducts fields and constructor.
    /// </summary>
    class OrderProducts : IOrder
    {
        public int OrderId { get; }
        public int OrderCustomerId { get; }
        public int ProductId { get; }
        public int ProductQty { get; }

        public OrderProducts()
        {

        }
    }
}
