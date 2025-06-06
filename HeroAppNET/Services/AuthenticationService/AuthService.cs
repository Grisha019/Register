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

        public AuthService()
        {
            _context = new ApplicationContext();
        }

        public async Task<bool> UserExistsAsync(string login, string email)
        {
            return await _context.Users.AnyAsync(u => u.Login == login || u.Email == email);
        }

        public async Task RegisterAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
