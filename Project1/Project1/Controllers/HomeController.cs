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

        public HomeController(ILogger<HomeController> logger, ICustomerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Index Action Method
        /// Will send user to login screen if not logged in, or display homepage
        /// </summary>
        /// <returns>Homepage or Login</returns>
        //Get Index
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

        /// <summary>
        /// Sends user to login page
        /// </summary>
        /// <returns>Login View</returns>
        //GET Login
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// If input is valid, will log the user in
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Back to login if error or to home page</returns>
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

        /// <summary>
        /// Sends user to register page
        /// </summary>
        /// <returns>Register View</returns>
        //GET Register
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// If input is valid, will create a new user and send the user to login page
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Login page or register again</returns>
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
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error registering user: " + e);
                }

                return RedirectToAction("Login");
            }

            return View();
        }

        /// <summary>
        /// Will show user the product page to add to cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product page</returns>
        //GET Product
        public IActionResult Product(int id)
        {
            ProductLocationViewModel model = new ProductLocationViewModel();
            Product p = _repository.GetProductFromId(id);
            model.locations = _repository.GetLocations().ToList();
            model.ProductId = p.ProductId;
            model.ProductName = p.ProductName;
            model.ProductPrice = p.ProductPrice;
            model.ProductImage = p.ProductImage;

            TempData["Success"] = "0";
            TempData.Keep("Success");

            return View(model);
        }

        /// <summary>
        /// If input is valid, adds product to cart
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Product page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Product(ProductLocationViewModel p)
        {
            if (ModelState.IsValid)
            {
                ProductLocationViewModel model = new ProductLocationViewModel();
                Product prod = _repository.GetProductFromId(p.ProductId);
                model.ProductId = prod.ProductId;
                model.ProductName = prod.ProductName;
                model.ProductPrice = prod.ProductPrice;
                model.ProductImage = prod.ProductImage;
                model.locations = _repository.GetLocations().ToList();


                var user_id = HttpContext.Request.Cookies["user_id"];
                Product item = new Product(p.ProductId, p.ProductName, p.ProductPrice, p.ProductQty, p.ProductImage);
                _repository.AddItemToCart(item, Int32.Parse(user_id), p.LocationId);

                TempData["Success"] = "1";
                TempData.Keep("Success");

                return View(model);
            }

            ModelState.AddModelError("", "Error adding item to cart");
            return View(p);
        }

        /// <summary>
        /// Logs user out
        /// </summary>
        /// <returns>Sends to login page</returns>
        //Logout
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Gets the users cart info
        /// </summary>
        /// <returns>Sends to cart view</returns>
        //Get Cart
        public IActionResult Cart()
        {
            //Get cart from db
            var user_id = HttpContext.Request.Cookies["user_id"];
            List<Product> cart = _repository.GetCart(Int32.Parse(user_id)).ToList();

            return View(cart);
        }

        /// <summary>
        /// Places an order
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Cart View</returns>
        //Post Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cart(List<Product> p)
        {
            //place order
            int order_id = _repository.GetLastOrderId();
            var user_id = HttpContext.Request.Cookies["user_id"];
            decimal total = 0;
            List<Product> cart = _repository.GetCart(Int32.Parse(user_id)).ToList();
            foreach (var item in cart)
            {
                total += item.ProductPrice * item.ProductQty;
            }

            Order order = new Order(order_id + 1, Int32.Parse(user_id), DateTime.Now, 1, total);
            _repository.PlaceOrder(order);

            //place order items
            int last_order = _repository.GetLastOrderId();
            _repository.PlaceOrderItems(cart, last_order);

            //clear cart
            cart.Clear();
            _repository.ClearCart(Int32.Parse(user_id));

            return View(cart);
        }

    }
}
