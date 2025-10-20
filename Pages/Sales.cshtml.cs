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
                // ������� ������ ��� �����������
                Sales = await _context.Sales
                    .Include(s => s.Employee)  // �������� ����������
                    .OrderByDescending(s => s.Date)  // ��������� �� ����
                    .ToListAsync();

                // �������� ��� �������
                Console.WriteLine($"��������� ������: {Sales.Count}");
                foreach (var sale in Sales.Take(3))
                {
                    Console.WriteLine($"Sale ID: {sale.Id}, Date: {sale.Date}, Employee: {sale.Employee?.FullName ?? "NULL"}, Total: {sale.TotalPrice}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������ ��� �������� ������: {ex.Message}");
                // ������������� ������ ������ � ������ ������
                Sales = new List<Sale>();
            }
        }
    }
}