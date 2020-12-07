using Project1.BusinessLibrary;
using System.Collections.Generic;

namespace Project1.WebApp.Models
{
    public class AdminUserModel
    {
        public List<Customer> Customers { get; set; }
        public string id { get; set; }

        public AdminUserModel()
        {
            Customers = new List<Customer>();
        }
    }
}
