using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apteka_razor.Pages
{
    public class AddEmployeeModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public Employee Employee { get; set; } = new();

        public string? Message { get; set; }

        public AddEmployeeModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            // Проверяем роль из сессии — только Admin может добавлять
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                Message = "❌ У вас нет прав для добавления сотрудников.";
                return Page();
            }

            _context.Employees.Add(Employee);
            _context.SaveChanges();

            Message = "✅ Сотрудник успешно добавлен!";
            Employee = new(); // очистить форму
            return Page();
        }
    }
}
