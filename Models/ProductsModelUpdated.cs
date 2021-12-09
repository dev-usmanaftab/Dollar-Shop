using DollarShop.Controllers;
using DollarShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Models
{
    public class ProductsModelUpdated
    {
        public string Name { get; set; }
        public CatagoryType Catagory { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int ProductID { get; set; }
    }
}
