using Microsoft.AspNetCore.Mvc;
using Moq;
using Project1.BusinessLibrary;
using Project1.BusinessLibrary.Interfaces;
using Project1.WebApp.Controllers;
using Project1.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project1.Testing
{
    public class AdminControllerTests
    {
        [Fact]
        public void DisplayLocations()
        {
            //arrange
            var mockRepository = new Mock<IAdminRepository>();

            //setup
            mockRepository.Setup(r => r.GetLocations(""))
                .Returns(new[] { new StoreLocation(1, "Store 1"), new StoreLocation(2, "Store 2") });

            var controller = new AdminController(mockRepository.Object);

            //act
            IActionResult actionResult = controller.Locations("");

            //assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var locations = Assert.IsAssignableFrom<AdminLocationModel>(viewResult.Model);
            Assert.Equal(1, locations.Locations[0].StoreLocationId);
            Assert.Equal("Store 2", locations.Locations[1].StoreLocationName);
        }

        [Fact]
        public void DisplayProducts()
        {
            //arrange
            var mockRepository = new Mock<IAdminRepository>();

            //setup
            mockRepository.Setup(r => r.GetProducts(""))
                .Returns(new[] { new Product(1, "p1", 10, 5, "image url") });

            var controller = new AdminController(mockRepository.Object);

            //act
            IActionResult actionResult = controller.Products("");

            //assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var products = Assert.IsAssignableFrom<AdminProductModel>(viewResult.Model);
            Assert.Equal(1, products.Products[0].ProductId);
            Assert.Equal("p1", products.Products[0].ProductName);
        }

        [Fact]
        public void DisplayUsers()
        {
            //arrange
            var mockRepository = new Mock<IAdminRepository>();

            //setup
            mockRepository.Setup(r => r.GetCustomers(""))
                .Returns(new[] { new Customer(1, "Test 1", "Test 2", 1, "test@test.com", "hash") });

            var controller = new AdminController(mockRepository.Object);

            //act
            IActionResult actionResult = controller.Users("");

            //assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var users = Assert.IsAssignableFrom<AdminUserModel>(viewResult.Model);
            Assert.Equal("Test 1", users.Customers[0].FirstName);
            Assert.Equal("Test 2", users.Customers[0].LastName);
        }

        [Fact]
        public void DisplayOrders()
        {
            //arrange
            var mockRepository = new Mock<IAdminRepository>();

            //setup
            mockRepository.Setup(r => r.GetOrders(""))
                .Returns(new[] { new Order(1, 1, DateTime.Now, 100) });

            var controller = new AdminController(mockRepository.Object);

            //act
            IActionResult actionResult = controller.Orders("");

            //assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var orders = Assert.IsAssignableFrom<AdminOrderModel>(viewResult.Model);
            Assert.Equal(100, orders.Orders[0].OrderTotal);
            Assert.Equal(1, orders.Orders[0].OrderId);
        }
    }
}
