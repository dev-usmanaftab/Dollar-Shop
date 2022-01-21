using DollarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Controllers
{
    public class RequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AjaxDemoResponseDTO
    {
        public string Msg { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("HomeController.Index method called!!!");
            return View();
        }

        public IActionResult SubmitDemo(RequestDTO data)
        {  
            
            if (data.Username == "usman" && data.Password == "12345")
            {
                return Json("success");
            }

            return Json("failed");
        }

        public IActionResult Products(string searchText="")
        {
            var products = new List<ProductsModelUpdated>();
            ProductsModelUpdated Jacket= new ProductsModelUpdated();
            ProductsModelUpdated Shirt = new ProductsModelUpdated();
            ProductsModelUpdated Shoes = new ProductsModelUpdated();
            
            Jacket.Name  = "jacket_A1";
            Jacket.Price = 50;
            products.Add(Jacket);
            
            Jacket = new ProductsModelUpdated();
            Jacket.Name = "jacket_A2";
            Jacket.Price = 100;
            products.Add(Jacket);
            
            Jacket = new ProductsModelUpdated();
            Jacket.Name = "jacket_A3";
            Jacket.Price = 150;
            products.Add(Jacket);

            Shirt.Name  = "Shirt_B1";
            Shirt.Price = 100;
            products.Add(Shirt);

            Shoes.Name  = "Shoes_C1";
            Shoes.Price = 80;
            products.Add(Shoes);
            
            if (searchText == null)
            {
                return PartialView("ProductList", products);
            }
            
            if (searchText != "")
            {
                var filteredProduct = products.Where(p => p.Name == searchText).ToList();

                var filteredProducts = from product in products
                                       where product.Name == searchText
                                       select product;
                var filtered = filteredProducts.ToList();
                
                return PartialView("ProductList", filtered);
            }

            return View(products);
        }

        public IActionResult AjaxDemo(RequestDTO test)
        {
            var keys = this.HttpContext.Request.Query.Keys;
            foreach (var key in keys)
            {
                var dat = this.HttpContext.Request.Query[key].ToString();
            }

            var data = new AjaxDemoResponseDTO()
            {
                Msg = "Login Fails",
                IsSuccess = false
            };

            if (test.Username == "Usman" && test.Password == "12345")
            {
                data.Msg = "Login Successful";
                data.IsSuccess = true;
                return Json(data);
            }
            return Json(data);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
