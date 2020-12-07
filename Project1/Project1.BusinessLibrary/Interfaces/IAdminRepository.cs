using System.Collections.Generic;

namespace Project1.BusinessLibrary.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<Order> GetOrders(string search);
        IEnumerable<Customer> GetCustomers(string search);
        IEnumerable<StoreLocation> GetLocations(string search);
        IEnumerable<Product> GetProducts(string search);
        IEnumerable<Product> GetOrderDetails(int id);
    }
}
