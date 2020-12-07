using Microsoft.EntityFrameworkCore;
using Project1.BusinessLibrary.Interfaces;
using Project1.DataAccess.Entities;
using System;
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

        public IEnumerable<Project1.BusinessLibrary.Order> GetOrders(string search)
        {
            // query from DB
            var entities = _context.Orders.ToList();
            IEnumerable<Order> orders = null;

            //Filter & Map to object
            if (!string.IsNullOrEmpty(search))
            {
                orders = entities.Select(e => new Project1.BusinessLibrary.Order(e.Id, e.UserId, e.OrderTime, e.StoreId, e.OrderTotal)).Where(e => e.OrderId == Int32.Parse(search));
            }
            else
            {
                orders = entities.Select(e => new Project1.BusinessLibrary.Order(e.Id, e.UserId, e.OrderTime, e.StoreId, e.OrderTotal));
            }

            return orders;
        }

        public IEnumerable<Project1.BusinessLibrary.Customer> GetCustomers(string search)
        {
            // query from DB
            var entities = _context.Users.ToList();
            IEnumerable<Customer> customers = null;

            //Filter & Map to object
            if (!string.IsNullOrEmpty(search))
            {
                customers = entities.Select(e => new Project1.BusinessLibrary.Customer(e.Id, e.FirstName, e.LastName, e.UserType, e.Email, e.PasswordHash)).Where(e => e.CustomerId == Int32.Parse(search));
            }
            else
            {
                customers = entities.Select(e => new Project1.BusinessLibrary.Customer(e.Id, e.FirstName, e.LastName, e.UserType, e.Email, e.PasswordHash));
            }

            return customers;
        }

        public IEnumerable<StoreLocation> GetLocations(string search)
        {
            // query from DB
            var entities = _context.StoreLocations.ToList();
            IEnumerable<StoreLocation> locations = null;
            
            //Filter & Map to object
            if (!string.IsNullOrEmpty(search))
            {
                locations = entities.Select(e => new Project1.BusinessLibrary.StoreLocation(e.Id, e.Name)).Where(e => e.StoreLocationId == Int32.Parse(search));
            }
            else
            {
                locations = entities.Select(e => new Project1.BusinessLibrary.StoreLocation(e.Id, e.Name));
            }

            return locations;
        }

        public IEnumerable<Product> GetProducts(string search)
        {
            // query from DB
            var entities = _context.Products.ToList();
            IEnumerable<Product> products = null;

            //Filter & Map to object
            if (!string.IsNullOrEmpty(search))
            {
                products = entities.Select(e => new Project1.BusinessLibrary.Product(e.Id, e.Name, e.Price, e.ProductImage)).Where(e => e.ProductId == Int32.Parse(search));
            }
            else
            {
                products = entities.Select(e => new Project1.BusinessLibrary.Product(e.Id, e.Name, e.Price, e.ProductImage));
            }

            return products;
        }

        public IEnumerable<Product> GetOrderDetails(int id)
        {
            List<Product> products = new List<Product>();
            var orderProducts = _context.OrderProducts.Include(p => p.Product).Where(o => o.OrderId == id);

            foreach(var item in orderProducts)
            {
                products.Add(new Project1.BusinessLibrary.Product(item.ProductId, item.Product.Name, item.ProductPricePaid, item.ProductQty, item.Product.ProductImage));
            }

            return products;
        }
    }
}
