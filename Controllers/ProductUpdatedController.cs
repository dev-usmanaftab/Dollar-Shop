using DollarShop.DB;
using DollarShop.Factories;
using DollarShop.Interfaces;
using DollarShop.Models;
using DollarShop.Models.DTOs.Product;
using DollarShop.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Controllers
{
    public class ProductUpdatedController : Controller
    {
        IProductRepository _repo;
        public DollarShopContext _context { get; set; }
        public ProductUpdatedController(IProductRepository repo, DollarShopContext context)
        {
            _repo = repo;
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        
        public IActionResult Search(SearchDTO data)
        {
            var products = _repo.GetProducts();

            // Filter Name
            if (!string.IsNullOrWhiteSpace(data.Name))
            {
                products = products.Where( p => p.Name.Contains(data.Name, StringComparison.InvariantCultureIgnoreCase) ).ToList();
            }

            // Filter Min Price
            if (data.Minprice.HasValue)
            {
                products = products.Where( p => p.Price >= data.Minprice.Value ).ToList();
            }
            
            // Filter Max Price
            if (data.Maxprice.HasValue)
            {
                products = products.Where ( p => p.Price <= data.Maxprice.Value ).ToList();
            }

            // Filter Category
            if(data.Catagory.HasValue)
            {
                if (data.Catagory.Value != CatagoryType.All)
                {
                    products = products.Where(p => p.Catagory == data.Catagory.Value).ToList();
                }
                
            }


            //To check whether any Product available in Product List
            if( products.Count == 0 )
            {
                var Data = new ResponseDTO(products);
                return Json(Data);
            }
            
            else
            {
                var Data = new ResponseDTO(products);
                return Json(Data);
            }
        }

        public IActionResult Products()
        {
            var products = _repo.GetProducts();
            return View( products );
        }

        public IActionResult ProductsFromJson()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////
            _context.Database.EnsureCreated();
            
            var products1 = _context.Products.ToList(); // To get all products


            ProductsModelUpdated Men = new ProductsModelUpdated();
            Men.Name = "Jacket Blue_dark";
            Men.Price = 250;
            Men.Catagory = CatagoryType.Men;
            Men.Image = "https://bit.ly/3csfAjt";


            _context.Products.Add(Men); // Add a product 
            _context.SaveChanges(); // Save data in database
            
            var products2 = _context.Products.ToList(); // To get all products
            products2.First().Name = "Jacket Blue Dark";
            _context.SaveChanges(); // Update data in database

            var products3 = _context.Products.ToList(); // To get all products
            
            _context.Products.Remove(Men);
            _context.SaveChanges(); // Update data in database
            
            var products4 = _context.Products.ToList(); // To get all products

            ///////////////////////////////////////////////////////////////////////////////////////////
            var products = _repo.GetProducts();
            return View(products);
        }

        [HttpPost]
        public IActionResult EditItem( EditRequestDTO item )
        {
            EditResponseDTO Data = new EditResponseDTO();

            if (String.IsNullOrWhiteSpace(item.Name))
            {
                Data.IsSuccess = false;
                Data.Message = "Please Enter A Valid Product Name";
                return Json(Data);
            }

            if (String.IsNullOrWhiteSpace(item.Image))
            {
                Data.IsSuccess = false;
                Data.Message = "Please Enter A Valid Image Link";
                return Json(Data);
            }

            if (!item.Catagory.HasValue)
            {
                Data.IsSuccess = false;
                Data.Message = "Please Enter A Valid Catagory Type";
                return Json(Data);
            }

            if (!item.Price.HasValue)
            {
                Data.IsSuccess = false;
                Data.Message = "Please Enter A Valid Price";
                return Json(Data);
            }

            var product         = new ProductsModelUpdated();
            product.Name        = item.Name;
            product.Price       = item.Price.Value;
            product.Image       = item.Image;
            product.Catagory    = item.Catagory.Value;
            product.ProductID   = item.ProductId.Value;

            Data.IsSuccess = _repo.EditProduct(product);
            
            if (!Data.IsSuccess)
            {
                Data.Message = "Product Not Exists";
                return Json(Data);
            }
            
            Data.Message   = "Product Updated !!!";
            return Json(Data);
        }

        public IActionResult EditItemPage(int? id)
        {
            if ( !id.HasValue )
            {
                return View(null);
            }

            var products = _repo.GetProducts();
            var Data = products.FirstOrDefault(P => P.ProductID == id.Value );
            return View(Data);
        }

        [HttpDelete]
        public IActionResult DelItem(DelRequestDTO item)
        {
            var Data = new DelResponseDTO();
            Data.IsSuccess = _repo.DeleteProduct(item.ProductId);
            if (Data.IsSuccess)
            {
                Data.Message = "Product Deleted !!!";
                return Json(Data);
            }
            
            Data.Message = "Product Not Found !!!";
            return Json(Data);
        }

        public IActionResult DelItemPage(int? id)
        {
            if (!id.HasValue)
            {
                return View(null);
            }

            var products = _repo.GetProducts();
            var item = products.FirstOrDefault(p => p.ProductID == id);
            return View(item);
        }

        public IActionResult AddItemPage( )
        { 
            return View();
        }

        [HttpPost]
        public IActionResult AddItem(AddRequestDTO item)
        {
            AddResponseDTO Data = new AddResponseDTO();

            if ( String.IsNullOrWhiteSpace(item.Name) )
                {
                    Data.IsSuccess = false;
                    Data.Message   = "Please Enter A Valid Product Name";
                    return Json(Data);
                }

            if (String.IsNullOrWhiteSpace(item.Image) )
                {
                    Data.IsSuccess = false;
                    Data.Message = "Please Enter A Valid Image Link";
                    return Json(Data);
                }
            
            if (!item.Catagory.HasValue)
                {
                    Data.IsSuccess = false;
                    Data.Message = "Please Enter A Valid Catagory Type";
                    return Json(Data);
                }

            if (!item.Price.HasValue)
            {
                Data.IsSuccess = false;
                Data.Message = "Please Enter A Valid Price";
                return Json(Data);
            }
            
            var NewProduct = new ProductsModelUpdated();
            NewProduct.Name      = item.Name;
            NewProduct.Image     = item.Image;
            NewProduct.Price     = item.Price.Value;
            NewProduct.Catagory  = item.Catagory.Value;
            NewProduct.ProductID = ProductRepository.GenerateID;
            
            _repo.AddProducts(NewProduct);
            Data.IsSuccess  =   true;
            Data.Message    =   "Product Added !!!";

            return Json(Data);
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
