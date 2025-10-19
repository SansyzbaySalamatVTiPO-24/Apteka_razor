using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data;
using Apteka_razor.Data.Models;

namespace Apteka_razor.Pages
{
    public class EmployeesModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Employee> Employees { get; set; } = new();

        public EmployeesModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Employees = _context.Employees
                .Include(e => e.Pharmacy)
                .ToList();
        }
    }
}
