using System.Collections.Generic;

namespace Project1.BusinessLibrary.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Product> GetProducts();
        string GetPassHash(string email);
        Customer GetCustomer(string email);
        IEnumerable<StoreLocation> GetLocations();
        void RegisterUser(Customer c);
        int GetLastCutomerId();
        Product GetProductFromId(int id);
        void AddItemToCart(Product p, int custId, int locationId);
        IEnumerable<Product> GetCart(int userid);
        void PlaceOrder(Order o);
        void PlaceOrderItems(List<Product> items, int lastid);
        int GetLastOrderId();
        void ClearCart(int userid);
    }
}
