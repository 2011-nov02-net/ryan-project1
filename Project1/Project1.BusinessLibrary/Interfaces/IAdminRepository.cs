using System.Collections.Generic;

namespace Project1.BusinessLibrary.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<StoreLocation> GetLocations();
        IEnumerable<Product> GetProducts();
    }
}
