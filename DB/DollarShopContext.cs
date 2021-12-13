using DollarShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DollarShop.DB
{
    public class DollarShopContext : DbContext
    {
        public DollarShopContext(DbContextOptions<DollarShopContext> options) : base(options)
        {
        }

        public DbSet<ProductsModelUpdated> Products { get; set; }

    }
}
