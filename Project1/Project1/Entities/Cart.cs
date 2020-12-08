using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.WebApp.Entities
{
    public partial class Cart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int ProductQty { get; set; }
        public decimal ProductPricePaid { get; set; }

        public virtual StoreLocation Location { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
