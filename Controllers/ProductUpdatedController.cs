using DollarShop.DB;
using DollarShop.Factories;
using DollarShop.Interfaces;
using DollarShop.Models;
using DollarShop.Models.DTOs.Product;
using DollarShop.Models.Enums;
using DollarShop.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Controllers
{
    public class ProductUpdatedController : Controller
    {
        IProductRepository _repo;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductUpdatedController(IProductRepository repo, IWebHostEnvironment hostEnvironment)
        {
            _repo = repo;
            _hostEnvironment = hostEnvironment;
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
            return View(products);
        }

        public IActionResult ProductsFromJson()
        {
            var products = _repo.GetProducts();
            return View(products);
        }

        [HttpPost]
        public IActionResult EditItem (EditRequestDTO item)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
            string extension = Path.GetExtension(item.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "\\Image\\", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                item.ImageFile.CopyTo(fileStream);
            }

            var Product = new ProductsModelUpdated();
            Product.Name = item.Name;
            Product.Image = Path.Combine("\\Image\\", fileName);
            Product.Price = item.Price.Value;
            Product.Catagory = item.Catagory.Value;
            Product.ProductID = item.ProductId.Value;
            
            EditResponseDTO Data = new EditResponseDTO();
            Data.IsSuccess = _repo.EditProduct(Product);
            Data.Message = "Product Updated!!!";
                        
            if (!Data.IsSuccess)
            {
                Data.Message = "Edit Failed...";
            }

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
        public async Task<IActionResult> Create (AddRequestDTO item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
                    string extension = Path.GetExtension(item.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string dir = Path.Combine(wwwRootPath,"Image");
                    string path = Path.Combine(dir, fileName);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await item.ImageFile.CopyToAsync(fileStream);
                    }

                    var NewProduct = new ProductsModelUpdated();
                    NewProduct.Name = item.Name;
                    NewProduct.Image = Path.Combine("\\Image\\", fileName);
                    NewProduct.Price = item.Price.Value;
                    NewProduct.Catagory = item.Catagory.Value;

                    _repo.AddProducts(NewProduct);
                }

                return RedirectToAction(nameof(ProductsFromJson));
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("logs.txt", ex.ToString());
                return View("Error");
            }
            
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
