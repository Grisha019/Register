using Microsoft.EntityFrameworkCore;
using HeroAppNET.Models;

namespace HeroAppNET.Infrastructure.ApplicationContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        // Конструктор для использования с EF Core CLI (мigrationBuilder)
        public ApplicationContext()
        {
        }

        // Конструктор для DI — принимает options из Program/Startup
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = System.IO.Path.GetFullPath("app.db");
                Console.WriteLine($"Using SQLite DB path: {dbPath}");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Здесь можно добавлять настройку модели
            base.OnModelCreating(modelBuilder);

            // Например, установить максимальную длину логина
            modelBuilder.Entity<UserModel>()
                .Property(u => u.Login)
                .HasMaxLength(100);

            modelBuilder.Entity<UserModel>()
                .Property(u => u.Email)
                .HasMaxLength(100);
        }
    }
}