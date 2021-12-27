using DollarShop.DB;
using DollarShop.Interfaces;
using DollarShop.Models;
using DollarShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Repositories
{
    public class ProductsFromDBRepository : IProductRepository
    {
        private DollarShopContext _context { get; set; }

        public ProductsFromDBRepository(DollarShopContext context)
        {
            _context = context;
        }

        public bool DeleteProduct(int? id)
        {
            var item = _context.Products.FirstOrDefault( P => P.ProductID == id );

            if ( item == null )
            {
                return false;
            }
            
            _context.Products.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public bool EditProduct(ProductsModelUpdated Item)
        {
            var product = _context.Products.FirstOrDefault(P => P.ProductID == Item.ProductID);

            if (product == null)
            {
                return false;
            }

            product.Name    = Item.Name;
            product.Image   = Item.Image;
            product.Price   = Item.Price;
            product.Catagory= Item.Catagory;
            
            _context.SaveChanges();

            return true;
        }

        public void AddProducts(ProductsModelUpdated NewItem)
        {
            _context.Database.EnsureCreated();
            _context.Products.Add(NewItem);
            _context.SaveChanges();
        }

        public List<ProductsModelUpdated> GetProducts()
        {
            _context.Database.EnsureCreated();

            if (_context.Products.Count() != 0)
            {
                return _context.Products.ToList();
            }

            var P = new ProductsModelUpdated();
            P.Name = "Jacket Blue_dark";
            P.Price = 250;
            P.Catagory = CatagoryType.Men;
            P.Image = "https://bit.ly/3csfAjt";
            _context.Products.Add(P);

            P = new ProductsModelUpdated();
            P.Name = "Shirt Brown";
            P.Price = 150;
            P.Catagory = CatagoryType.Men;
            P.Image = "https://bit.ly/3FzKFOH";
            _context.Products.Add(P);

            P = new ProductsModelUpdated();
            P.Name = "Saree light-green";
            P.Price = 150;
            P.Catagory = CatagoryType.Women;
            P.Image = "https://bit.ly/3p2dugr";
            _context.Products.Add(P);

            P = new ProductsModelUpdated();
            P.Name = "Trouser Black";
            P.Price = 200;
            P.Catagory = CatagoryType.Women;
            P.Image = "https://bit.ly/30KMuK6";
            _context.Products.Add(P);

            P = new ProductsModelUpdated();
            P.Name = "Toy Car";
            P.Price = 100;
            P.Catagory = CatagoryType.Children;
            P.Image = "https://bit.ly/3FMOCjx";
            _context.Products.Add(P);

            P = new ProductsModelUpdated();
            P.Name = "Baby Frock Red";
            P.Price = 100;
            P.Catagory = CatagoryType.Children;
            P.Image = "https://bit.ly/30JlbQk";
            _context.Products.Add(P);
            
            _context.SaveChanges();

            return _context.Products.ToList();
        }
    }
}