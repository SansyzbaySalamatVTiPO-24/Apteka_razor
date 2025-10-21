using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

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
                Console.WriteLine("=== Начало загрузки продаж ===");

                // Проверяем подключение к базе данных
                bool canConnect = await _context.Database.CanConnectAsync();
                Console.WriteLine($"Подключение к базе данных: {(canConnect ? "успешно" : "ошибка")}");
                Console.WriteLine($"База данных: {_context.Database.GetDbConnection().Database}");
                Console.WriteLine($"Сервер: {_context.Database.GetDbConnection().DataSource}");

                // ✅ Исправленный запрос
                Sales = await _context.Sales
                    .Include(s => s.Employee) // подключаем сотрудника
                    .OrderByDescending(s => s.SaleDate)
                    .ToListAsync();

                Console.WriteLine($"Загружено продаж: {Sales.Count}");

                if (Sales.Count == 0)
                {
                    Console.WriteLine("⚠️ Продажи не найдены. Возможно, таблица пустая или подключение к другой БД.");
                }
                else
                {
                    foreach (var sale in Sales.Take(3))
                    {
                        Console.WriteLine($"Sale ID: {sale.Id}, Date: {sale.SaleDate}, Employee: {sale.Employee?.FullName ?? "NULL"}, Total: {sale.TotalPrice}");
                    }
                }

                Console.WriteLine("=== Загрузка продаж завершена ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при загрузке продаж: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Sales = new List<Sale>();
            }
        }
    }
}
