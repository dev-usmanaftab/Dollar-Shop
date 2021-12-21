using DollarShop.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.Models.DTOs.Product
{
    public class AddRequestDTO
    {
        public string Name { get; set; }
        public int? Price { get; set; }
        public CatagoryType? Catagory { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
