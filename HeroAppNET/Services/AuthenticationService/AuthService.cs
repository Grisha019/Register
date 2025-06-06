using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Models;
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

        public bool UserExists(string login, string email)
        {
            return _context.Users.Any(u => u.Login == login || u.Email == email);
        }

        public void Register(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
