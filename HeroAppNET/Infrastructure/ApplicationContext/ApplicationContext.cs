using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroAppNET.Models;
using HeroAppNET.Models.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HeroAppNET.Infrastructure.ApplicationContext
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

           
   
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<HealPotion> HealPotions { get; set; }

    }
}
