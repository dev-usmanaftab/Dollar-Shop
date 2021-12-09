using DollarShop.Controllers;
using DollarShop.Interfaces;
using DollarShop.Models;
using DollarShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Factories
{
    public class ProductRepository : IProductRepository
    {
        public static int GenerateID = 10;
        private static List<ProductsModelUpdated> _products;

        public bool DeleteProduct(int? id)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].ProductID == id)
                {
                    _products.Remove(_products[i]);
                    return true;
                }
            }
            
            return false;
        }

        public bool EditProduct(ProductsModelUpdated Item)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].ProductID == Item.ProductID)
                {
                    _products[i] = Item;
                    return true;
                }
            }
            
            return false;
        }

        public void AddProducts(ProductsModelUpdated NewItem) 
        {
             GenerateID++;
            _products.Add(NewItem); 
        }
        
        public List<ProductsModelUpdated> GetProducts()
        {
            if ( _products != null )
            {
                return _products;
            }
            _products = new List<ProductsModelUpdated>();
            ProductsModelUpdated Men   = new ProductsModelUpdated();
            ProductsModelUpdated Women = new ProductsModelUpdated();
            ProductsModelUpdated Child = new ProductsModelUpdated();

            Men.Name = "Jacket Blue_dark";
            Men.Price = 250;
            Men.Catagory = CatagoryType.Men;
            Men.Image = "https://bit.ly/3csfAjt";
            Men.ProductID = 1;
            _products.Add(Men);

            Men = new ProductsModelUpdated();
            Men.Name = "Shirt Brown";
            Men.Price = 150;
            Men.Catagory = CatagoryType.Men;
            Men.Image = "https://bit.ly/3FzKFOH";
            Men.ProductID = 2;
            _products.Add(Men);

            Women.Name = "Saree light-green";
            Women.Price = 150;
            Women.Catagory = CatagoryType.Women;
            Women.Image = "https://bit.ly/3p2dugr";
            Women.ProductID = 3;
            _products.Add(Women);

            Women = new ProductsModelUpdated();
            Women.Name = "Trouser Black";
            Women.Price = 200;
            Women.Catagory = CatagoryType.Women;
            Women.Image = "https://bit.ly/30KMuK6";
            Women.ProductID = 4;
            _products.Add(Women);

            Child.Name = "Toy Car";
            Child.Price = 100;
            Child.Catagory = CatagoryType.Children;
            Child.Image = "https://bit.ly/3FMOCjx";
            Child.ProductID = 5;
            _products.Add(Child);

            Child = new ProductsModelUpdated();
            Child.Name = "Baby Frock Red";
            Child.Price = 100;
            Child.Catagory = CatagoryType.Children;
            Child.Image = "https://bit.ly/30JlbQk";
            Child.ProductID = 6;
            _products.Add(Child);

            return _products;
        }
    }
}
