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
    }
}
