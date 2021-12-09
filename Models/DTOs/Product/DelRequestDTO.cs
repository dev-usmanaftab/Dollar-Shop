using DollarShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Models.DTOs.Product
{
    public class DelRequestDTO
    {
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Image { get; set; }
        public CatagoryType? Catagory { get; set; }
        public int? ProductId { get; set; }
    }
}
