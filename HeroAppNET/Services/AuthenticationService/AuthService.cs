using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HeroAppNET.Services.AuthenticationService
{
    public class AuthService
    {
        private readonly ApplicationContext _context;

        // Конструктор с DI для контекста базы данных  
        public AuthService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Проверяет, существует ли пользователь с таким логином или email.
        /// </summary>
        public async Task<bool> UserExistsAsync(string login, string email)
        {
            if (string.IsNullOrWhiteSpace(login) && string.IsNullOrWhiteSpace(email))
                return false;

            var userWithLogin = await _context.Users
                .Where(u => u.Login == login)
                .FirstOrDefaultAsync();

            if (userWithLogin != null)
                return true;

            var userWithEmail = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return userWithEmail != null;
        }

        /// <summary>
        /// Асинхронно регистрирует нового пользователя.
        /// </summary>
        public async Task RegisterAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UserExistsAsync(string login)
        {
            // Пример: проверяем, есть ли пользователь с таким логином в базе данных
            return await _context.Users.AnyAsync(u => u.Login == login);
        }

        public async Task<bool> CheckPasswordAsync(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null)
                return false;

            // Если у тебя хранится хеш пароля, то здесь надо сделать проверку хеша, 
            // для примера просто сравним строки
            return user.Password == password;
        }

        /// <summary>
        /// Проверяет учетные данные пользователя.
        /// </summary>
        public async Task<bool> AuthenticateAsync(string login, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == login);

            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}