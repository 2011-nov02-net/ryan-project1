using Microsoft.AspNetCore.Mvc;
using Project1.BusinessLibrary.Interfaces;

namespace Project1.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _repository;

        public AdminController(IAdminRepository repository)
        {
            _repository = repository;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Orders
        public ActionResult Orders()
        {
            var orders = _repository.GetOrders();
            return View(orders);
        }

        // GET: AdminController/Users
        public ActionResult Users()
        {
            var customers = _repository.GetCustomers();
            return View(customers);
        }

        // GET: AdminController/Locations
        public ActionResult Locations()
        {
            var locations = _repository.GetLocations();
            return View(locations);
        }

        // GET: AdminController
        public ActionResult Products()
        {
            var products = _repository.GetProducts();
            return View(products);
        }
    }
}
