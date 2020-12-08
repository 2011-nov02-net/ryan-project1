using Microsoft.EntityFrameworkCore;
using Project1.BusinessLibrary.Interfaces;
using Project1.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.BusinessLibrary
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly project1Context _context;

        public CustomerRepository(project1Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all the products
        /// </summary>
        public IEnumerable<Product> GetProducts()
        {
            // query from DB
            var entities = _context.Products.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.Product(e.Id, e.Name, e.Price, e.ProductImage));
        }

        /// <summary>
        /// Returns a users password hash
        /// </summary>
        /// <param name="email"></param>
        public string GetPassHash(string email)
        {
            //query db
            var entities = _context.Users.Where(x => x.Email == email).Select(x => x.PasswordHash);
            string hash = entities.First();

            return hash;
        }

        /// <summary>
        /// Returns a customer
        /// </summary>
        /// <param name="email"></param>
        public Customer GetCustomer(string email)
        {
            Customer customer = null;
            var entities = _context.Users.Where(x => x.Email == email);
            foreach (var c in entities)
            {
                customer = new Customer(c.Id, c.FirstName, c.LastName, c.UserType);
            }

            return customer;
        }

        /// <summary>
        /// Returns a list of locations
        /// </summary>
        public IEnumerable<StoreLocation> GetLocations()
        {
            // query from DB
            var entities = _context.StoreLocations.ToList();

            // map to business model
            return entities.Select(e => new Project1.BusinessLibrary.StoreLocation(e.Id, e.Name));
        }

        /// <summary>
        /// Creates a new user in the db
        /// </summary>
        /// <param name="c"></param>
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
        }

        /// <summary>
        /// Returns the last user in db id
        /// </summary>
        public int GetLastCutomerId()
        {
            var entities = _context.Users.OrderByDescending(x => x.Id);
            var last = entities.First();
            return last.Id;
        }

        /// <summary>
        /// Returns a product with same id
        /// </summary>
        /// <param name="id"></param>
        public Product GetProductFromId(int id)
        {
            var entities = _context.Products.ToList();
            var p = entities.Where(x => x.Id == id).First();

            return new Product(p.Id, p.Name, p.Price, p.ProductImage);
        }

        /// <summary>
        /// Adds an item to the cart db
        /// </summary>
        /// <param name="p"></param>
        /// <param name="custId"></param>
        /// <param name="locationId"></param>
        public void AddItemToCart(Product p, int custId, int locationId)
        {
            Cart cart = new Cart();
            cart.UserId = custId;
            cart.ProductId = p.ProductId;
            cart.LocationId = locationId;
            cart.ProductQty = p.ProductQty;
            cart.ProductPricePaid = p.ProductPrice;

            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets a list of cart products
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetCart(int userid)
        {
            var entities = _context.Carts.Where(x => x.UserId == userid);

            return entities.Select(e => new Product(e.ProductId, e.Product.Name, e.ProductPricePaid, e.ProductQty, e.LocationId));
        }

        /// <summary>
        /// Gets the last orders id
        /// </summary>
        /// <returns></returns>
        public int GetLastOrderId()
        {
            var entities = _context.Orders.OrderByDescending(x => x.Id);
            var last = entities.First();
            return last.Id;
        }

        /// <summary>
        /// Places an order to db
        /// </summary>
        /// <param name="o"></param>
        public void PlaceOrder(Order o)
        {
            Project1.DataAccess.Entities.Order order = new Project1.DataAccess.Entities.Order();
            order.UserId = o.OrderCustomerId;
            order.StoreId = o.OrderStoreLocationId;
            order.OrderTime = o.OrderTime;
            order.OrderTotal = o.OrderTotal;

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        /// <summary>
        /// Places the orders items to db
        /// </summary>
        /// <param name="items"></param>
        /// <param name="lastId"></param>
        public void PlaceOrderItems(List<Product> items, int lastId)
        {
            foreach(var item in items)
            {
                Project1.DataAccess.Entities.OrderProduct op = new Project1.DataAccess.Entities.OrderProduct();
                op.OrderId = lastId;
                op.ProductId = item.ProductId;
                op.ProductQty = item.ProductQty;
                op.ProductPricePaid = item.ProductPrice;

                _context.OrderProducts.Add(op);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes users cart items from db
        /// </summary>
        /// <param name="userid"></param>
        public void ClearCart(int userid)
        {
            var cartItems = _context.Carts.Where(x => x.UserId == userid);
            _context.Carts.RemoveRange(cartItems);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.ToList().ForEach(x => x.State = EntityState.Detached);
            }
        }
    }
}
