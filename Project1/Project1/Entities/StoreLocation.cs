using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.WebApp.Entities
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            StoreInventories = new HashSet<StoreInventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
