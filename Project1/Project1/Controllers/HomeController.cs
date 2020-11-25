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

        public IActionResult Index(UserModel user)
        {
            if (TempData["CustomerId"] != null)
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
            Console.WriteLine("\ninvalid\n");
            if (ModelState.IsValid)
            {
                var userDbHash = _repository.GetPassHash(user.Email);
                bool verify = BCrypt.Net.BCrypt.Verify(user.Password, userDbHash);
                Console.WriteLine("\npassword\n");
                if (verify == true)
                {
                    //success
                    Customer c = _repository.GetCustomer(user.Email);
                    TempData["CustomerId"] = c.CustomerId;
                    ViewData["FirstName"] = c.FirstName;
                    TempData.Keep("CustomerId");
                    Console.WriteLine("\ngood\n");
                    return RedirectToAction("Index");
                }
                else
                {
                    //incorrect password
                    Console.WriteLine("\nbad\n");
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

        public IActionResult Logout()
        {
            ViewData.Clear();
            return RedirectToAction("Login");
        }
    }
}
