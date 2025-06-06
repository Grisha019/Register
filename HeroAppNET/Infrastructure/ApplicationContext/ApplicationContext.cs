using HeroAppNET.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroAppNET.Infrastructure.ApplicationContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        // Конструктор без параметров нужен только если ты используешь PMC или Add-Migration
        public ApplicationContext()
            : base(GetOptions())
        {
        }

        private static DbContextOptions<ApplicationContext> GetOptions()
        {
            return new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=WPF;Username=postgres;Password=123")
                .Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Тут можно добавить настройку моделей, если нужно
        }

        public DbSet<UserModel> Users { get; set; }
    }
}