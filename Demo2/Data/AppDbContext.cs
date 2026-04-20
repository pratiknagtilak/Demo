using Demo2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Demo2.Data
{
    public class AppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
