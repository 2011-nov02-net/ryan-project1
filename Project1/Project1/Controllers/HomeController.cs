using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.BusinessLibrary;
using Project1.BusinessLibrary.Interfaces;
using Project1.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepository _repository;

        private List<Product> cart = new List<Product>();

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
                if (verify)
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

            return View();
        }

        //GET Register
        public IActionResult Register()
        {
            return View();
        }

        //POST Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserRegisterModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int lastId = _repository.GetLastCutomerId();
                    var hash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    Customer c = new Customer(lastId + 1, user.FirstName, user.LastName, 1, user.Email, hash);

                    _repository.RegisterUser(c);

                    Console.WriteLine("Created User");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error registering user: " + e);
                }

                return RedirectToAction("Login");
            }

            return View();
        }

        //GET Product
        public IActionResult Product(int id)
        {
            Console.WriteLine(cart.Count);
            ProductLocationViewModel model = new ProductLocationViewModel();
            Product p = _repository.GetProductFromId(id);
            model.locations = _repository.GetLocations().ToList();
            model.ProductId = p.ProductId;
            model.ProductName = p.ProductName;
            model.ProductPrice= p.ProductPrice;
            model.ProductImage = p.ProductImage;
            

            ViewBag.Success = false;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Product(ProductLocationViewModel p)
        {
            if (ModelState.IsValid)
            {
                Product item = _repository.GetProductFromId(p.ProductId);
                item.ProductQty = p.ProductQty;
                cart.Add(item);
                ViewBag.Success = true;
                Console.WriteLine(cart.Count);

                ProductLocationViewModel model = p;

                return View(model);
            }

            ModelState.AddModelError("", "Error adding item to cart");
            return View(p);
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
        public IActionResult Cart()
        {
            Console.WriteLine(cart.Count);
            cart.Add(new Product(1, "cyberpunk", 59, 1, "https://upload.wikimedia.org/wikipedia/en/9/9f/Cyberpunk_2077_box_art.jpg"));
            Console.WriteLine(cart.Count);
            return View(cart);
        }

        //Post Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cart(List<Product> p)
        {
            
            return View();
        }

    }
}
