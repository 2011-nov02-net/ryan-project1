using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.DataAccess.Entities
{
    public partial class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductQty { get; set; }
        public decimal ProductPricePaid { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
