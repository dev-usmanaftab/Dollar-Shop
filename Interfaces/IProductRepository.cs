using DollarShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Interfaces
{
    public interface IProductRepository
    {
        void AddProducts(ProductsModelUpdated NewItem);
        List<ProductsModelUpdated> GetProducts();
        bool EditProduct(ProductsModelUpdated Item);
        bool DeleteProduct(int? id);
    }
}
