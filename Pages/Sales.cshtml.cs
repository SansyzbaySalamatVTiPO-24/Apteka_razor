using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data;
using Apteka_razor.Data.Models;

namespace Apteka_razor.Pages
{
    public class SalesModel : PageModel
    {
        private readonly AppDbContext _context;

        public SalesModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Sale> Sales { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                // Загружаем все продажи без фильтров
                Sales = await _context.Sales
                    .AsNoTracking()
                    .OrderByDescending(s => s.SaleDate)
                    .ToListAsync();

                // Подгружаем сотрудников отдельно
                var employeeIds = Sales.Select(s => s.EmployeeId).Distinct();
                var employees = await _context.Employees
                    .Where(e => employeeIds.Contains(e.Id))
                    .ToListAsync();

                foreach (var sale in Sales)
                {
                    sale.Employee = employees.FirstOrDefault(e => e.Id == sale.EmployeeId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке продаж: {ex.Message}");
                Sales = new List<Sale>();
            }
        }
    }
}
