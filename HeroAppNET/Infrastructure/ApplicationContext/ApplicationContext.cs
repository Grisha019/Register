using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroAppNET.Models;
using HeroAppNET.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace HeroAppNET.Infrastructure.ApplicationContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            

   
            this.SaveChanges();
        }

        public DbSet<User> Users { get; set; }

    }
}
