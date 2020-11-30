using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BusinessLibrary.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Product> GetProducts();
        string GetPassHash(string email);
        Customer GetCustomer(string email);
        IEnumerable<StoreLocation> GetLocations();
    }
}
