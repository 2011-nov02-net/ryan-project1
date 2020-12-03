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
    }
}
