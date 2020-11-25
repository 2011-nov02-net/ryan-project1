using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLibrary.Interfaces
{
    public interface IOrder
    {
        public int OrderId { get; }
        public int OrderCustomerId { get; }
    }
}
