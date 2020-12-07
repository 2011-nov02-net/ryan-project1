using Microsoft.AspNetCore.Mvc;
using Project1.BusinessLibrary.Interfaces;
using Project1.WebApp.Models;
using System.Linq;

namespace Project1.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _repository;

        public AdminController(IAdminRepository repository)
        {
            _repository = repository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Orders
        public ActionResult Orders(string id)
        {
            AdminOrderModel model = new AdminOrderModel();
            var orders = _repository.GetOrders(id);

            model.Orders = orders.ToList();
            return View(model);
        }

        // GET: Admin/Users
        public ActionResult Users(string id)
        {
            AdminUserModel model = new AdminUserModel();
            var customers = _repository.GetCustomers(id);

            model.Customers = customers.ToList();
            return View(model);
        }

        // GET: Admin/Locations
        public ActionResult Locations(string id)
        {
            AdminLocationModel model = new AdminLocationModel();
            var locations = _repository.GetLocations(id);

            model.Locations = locations.ToList();
            return View(model);
        }

        // GET: Admin/Products
        public ActionResult Products(string id)
        {
            AdminProductModel model = new AdminProductModel();
            var products = _repository.GetProducts(id);

            model.Products = products.ToList();
            return View(model);
        }

        public ActionResult OrderDetails(int id)
        {
            var details = _repository.GetOrderDetails(id);

            return View(details);
        }
    }
}
