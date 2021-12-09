using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DollarShop.Controllers.ProductUpdatedController;

namespace DollarShop.Models
{
    public class ProductsModel
    {
        public string Name { get; set; }
        public string Catagory { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
    }
}
