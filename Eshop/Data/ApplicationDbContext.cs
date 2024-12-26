using Eshop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Eshop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product>Products {get;set;}
        public DbSet<Color> Colors { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<Category> Categorys { get; set; }

    }
}