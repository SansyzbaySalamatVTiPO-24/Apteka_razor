// Pages/SalesModel.cshtml.cs
using Microsoft.AspNetCore.Mvc;
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
                // Простой запрос для диагностики
                Sales = await _context.Sales
                    .Include(s => s.Employee)  // Включаем сотрудника
                    .OrderByDescending(s => s.Date)  // Сортируем по дате
                    .ToListAsync();

                // Логируем для отладки
                Console.WriteLine($"Загружено продаж: {Sales.Count}");
                foreach (var sale in Sales.Take(3))
                {
                    Console.WriteLine($"Sale ID: {sale.Id}, Date: {sale.Date}, Employee: {sale.Employee?.FullName ?? "NULL"}, Total: {sale.TotalPrice}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке продаж: {ex.Message}");
                // Устанавливаем пустой список в случае ошибки
                Sales = new List<Sale>();
            }
        }
    }
}