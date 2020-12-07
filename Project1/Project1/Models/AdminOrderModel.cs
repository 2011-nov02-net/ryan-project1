using Project1.BusinessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.Models
{
    public class AdminOrderModel
    {
        public List<Order> Orders { get; set; }
        public string id { get; set; }

        public AdminOrderModel()
        {
            Orders = new List<Order>();
        }
    }
}