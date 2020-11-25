using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.BusinessLibrary.Interfaces;
using Project1.DataAccess.Entities;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            var products = _repository.GetProducts();
            return View(products);
        }
    }
}
