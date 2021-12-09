using DollarShop.Controllers;
using DollarShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Models.DTOs.Product
{
    public class SearchDTO
    {
        public string Name { get; set; }
        public double? Minprice { get; set; }
        public double? Maxprice { get; set; }
        public CatagoryType? Catagory { get; set; }
    }
}
