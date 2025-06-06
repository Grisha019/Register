using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Services.AuthenticationService
{
    public class AuthService
    {
        private readonly ApplicationContext _context;

        public AuthService(ApplicationContext context)
        {
            _context = context;
        }

        public AuthService()
        {
        }

        public async Task<bool> UserExistsAsync(string login, string email)
        {
            // Assuming _context.Users is a DbSet<UserModel>  
            return await _context.Users.AnyAsync(u => u.Login == login || u.Email == email);
        }

        public async Task RegisterAsync(UserModel user)
        {
            // Assuming _context.Users is a DbSet<UserModel>  
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public bool UserExists(string login, string email)
        {
            return _context.Users.Any(u => u.Login == login || u.Email == email);
        }

        public void Register(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        internal bool Authenticate(Action login, string password)
        {
            // Implementation here  
            return false;
        }

        internal bool Authenticate(string loginField, string password)
        {
            // Implementation here  
            return false;
        }
    }
}
