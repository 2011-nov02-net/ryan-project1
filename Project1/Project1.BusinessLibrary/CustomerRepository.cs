using Project1.BusinessLibrary.Interfaces;
using Project1.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.BusinessLibrary
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly project1Context _context;

        public CustomerRepository(project1Context context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            // query from DB
            var entities = _context.Products.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.Product(e.Id, e.Name, e.Price, e.ProductImage));
        }

        public string GetPassHash(string email)
        {
            //query db
            var entities = _context.Users.Where(x => x.Email == email).Select(x => x.PasswordHash);
            string hash = entities.First();

            return hash;
        }

        public Customer GetCustomer(string email)
        {
            Customer customer = null;
            var entities = _context.Users.Where(x => x.Email == email);
            foreach(var c in entities)
            {
                customer = new Customer(c.Id, c.FirstName, c.LastName, c.UserType);
            }

            return customer;
        }

        public IEnumerable<StoreLocation> GetLocations()
        {
            // query from DB
            var entities = _context.StoreLocations.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.StoreLocation(e.Id, e.Name));
        }

        public void RegisterUser(Customer c)
        {
            User newUser = new User();
            newUser.FirstName = c.FirstName;
            newUser.LastName = c.LastName;
            newUser.Email = c.Email;
            newUser.UserType = 1;
            newUser.PasswordHash = c.PasswordHash;

            _context.Users.Add(newUser);
            _context.SaveChanges();
            Console.WriteLine("Created User in repo");
        }

        public int GetLastCutomerId()
        {
            var entities = _context.Users.OrderByDescending(x => x.Id);
            var last = entities.First();
            return last.Id;
        }
    }
}
