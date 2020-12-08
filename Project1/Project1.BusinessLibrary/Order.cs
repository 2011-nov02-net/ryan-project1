using Project1.BusinessLibrary.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Project1.BusinessLibrary
{
    /// <summary>
    /// Order class. Contains orders fields and constructor
    /// </summary>
    public class Order : IOrder
    {
        [Display(Name = "Order Id")]
        public int OrderId { get; }

        [Display(Name = "Customer Id")]
        public int OrderCustomerId { get; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { get; }
        public int OrderStoreLocationId { get; }

        [Display(Name = "Order Total")]
        public decimal OrderTotal { get; }

        public Order(int id, Project1.BusinessLibrary.Customer cust, DateTime time, Project1.BusinessLibrary.StoreLocation location, decimal total)
        {
            OrderId = id;
            OrderCustomerId = cust.CustomerId;
            OrderTime = time;
            OrderStoreLocationId = location.StoreLocationId;
            OrderTotal = total;
        }

        public Order(int id, int custId, DateTime time, int locationId, decimal total)
        {
            OrderId = id;
            OrderCustomerId = custId;
            OrderTime = time;
            OrderStoreLocationId = locationId;
            OrderTotal = total;
        }

        public Order(int id, int custId, DateTime time, decimal total)
        {
            OrderId = id;
            OrderCustomerId = custId;
            OrderTime = time;
            OrderTotal = total;
        }
    }
}
