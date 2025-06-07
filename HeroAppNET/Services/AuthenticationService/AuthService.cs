using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Windows;

namespace HeroAppNET.Services.AuthenticationService
{
    public class AuthService
    {
        private readonly ApplicationContext _context;

        public AuthService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string login, string email)
        {
            return await _context.Users.AnyAsync(u => u.Login == login || u.Email == email);
        }

        public async Task RegisterAsync(UserModel user)
        {
            // ВАЖНО: Хешировать только если пароль ещё не хеширован!
            if (!user.Password.StartsWith("$2a$")) // признак BCrypt
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string login)
        {
            return await _context.Users.AnyAsync(u => u.Login == login);
        }

        public async Task<bool> CheckPasswordAsync(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден.");
                return false;
            }

            // Для отладки — смотри хеш и ввод
            MessageBox.Show($"Пароль из базы: {user.Password}\nВведённый: {password}");

            // Сравниваем хеш
            bool verified = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!verified)
                MessageBox.Show("BCrypt.Verify вернул false");

            return verified;
        }

        public async Task<bool> AuthenticateAsync(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
                return false;
            }

            MessageBox.Show($"Пароль из базы:\n{user.Password}\nВведённый: {password}");

            bool result = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!result)
                MessageBox.Show("Неверный пароль");

            return result;
        }
    }
}