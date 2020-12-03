using Project1.BusinessLibrary.Interfaces;
using Project1.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Project1.BusinessLibrary
{
    public class AdminRepository : IAdminRepository
    {
        private readonly project1Context _context;

        public AdminRepository(project1Context context)
        {
            _context = context;
        }

        public IEnumerable<Project1.BusinessLibrary.Order> GetOrders()
        {
            // query from DB
            var entities = _context.Orders.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.Order(e.Id, e.UserId, e.OrderTime, e.StoreId, e.OrderTotal));
        }

        public IEnumerable<Project1.BusinessLibrary.Customer> GetCustomers()
        {
            // query from DB
            var entities = _context.Users.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.Customer(e.Id, e.FirstName, e.LastName, e.UserType, e.Email, e.PasswordHash));
        }

        public IEnumerable<StoreLocation> GetLocations()
        {
            // query from DB
            var entities = _context.StoreLocations.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.StoreLocation(e.Id, e.Name));
        }

        public IEnumerable<Product> GetProducts()
        {
            // query from DB
            var entities = _context.Products.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.Product(e.Id, e.Name, e.Price, e.ProductImage));
        }
    }
}
