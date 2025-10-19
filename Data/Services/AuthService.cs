using Apteka_razor.Data.Models;

namespace Apteka_razor.Data.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        // Проверка логина и пароля
        public Employee? Authenticate(string login, string password)
        {
            return _context.Employees
                .FirstOrDefault(e => e.Login == login && e.Password == password);
        }
    }
}
