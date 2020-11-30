using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.BusinessLibrary.Interfaces;
using Project1.DataAccess.Entities;
using Project1.Models;
using Project1.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BCrypt;
using Project1.BusinessLibrary;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepository _repository;

        public HomeController(ILogger<HomeController> logger, ICustomerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies["user_id"] != null)
            {
                var products = _repository.GetProducts();
                return View(products);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET Login
        public IActionResult Login()
        {
            return View();
        }

        //POST Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var userDbHash = _repository.GetPassHash(user.Email);
                bool verify = BCrypt.Net.BCrypt.Verify(user.Password, userDbHash);
                if (verify == true)
                {
                    //success
                    Customer c = _repository.GetCustomer(user.Email);
                    ViewData["FirstName"] = c.FirstName;
                    HttpContext.Response.Cookies.Append("user_id", c.CustomerId.ToString());
                    HttpContext.Response.Cookies.Append("user_type", c.UserType.ToString());
                    HttpContext.Response.Cookies.Append("user_name", c.FirstName);
                    return RedirectToAction("Index");
                }
                else
                {
                    //incorrect password
                    ModelState.AddModelError("Password", "Incorrect Password.");
                    return View();
                }
            }
            Console.WriteLine();
            return View();
        }

        //GET Register
        public IActionResult Register()
        {
            return View();
        }

        //GET Product
        public IActionResult Product(int id)
        {
            var entity = _repository.GetProducts();

            ProductLocationViewModel model = new ProductLocationViewModel();
            model.locations = _repository.GetLocations().ToList();
            model.product = entity.First(x => x.ProductId == id);

            return View(model);
        }

        //Logout
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login");
        }

        //Get Cart
        public IActionResult Cart(List<BusinessLibrary.Product> products)
        {
            return View(products);
        }

    }
}
