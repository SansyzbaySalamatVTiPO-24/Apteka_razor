using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Apteka_razor.Pages
{
    public class AddSaleModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public Sale Sale { get; set; } = new();

        [BindProperty]
        public List<SaleDetail> SaleDetails { get; set; } = new() { new SaleDetail() };

        public List<Employee> Employees { get; set; } = new();
        public List<Drug> Drugs { get; set; } = new();

        public string? Message { get; set; }

        public AddSaleModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Employees = _context.Employees.ToList();
            Drugs = _context.Drugs.ToList();
        }

        public IActionResult OnPost()
        {
            // Проверка роли — только Admin и Pharmacist могут добавлять
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && role != "Pharmacist")
            {
                Message = "❌ У вас нет прав для добавления продаж.";
                Employees = _context.Employees.ToList();
                Drugs = _context.Drugs.ToList();
                return Page();
            }

            // Если дата не задана, ставим текущую
            if (Sale.Date == default)
                Sale.Date = DateTime.Now;

            // Вычисляем цену для каждой позиции
            foreach (var detail in SaleDetails)
            {
                var drug = _context.Drugs.FirstOrDefault(d => d.Id == detail.DrugId);
                if (drug != null)
                {
                    detail.Price = (double)drug.Price * detail.Quantity;
                }
            }

            Sale.TotalPrice = (decimal)SaleDetails.Sum(d => d.Price);
            Sale.SaleDetails = SaleDetails;

            _context.Sales.Add(Sale);
            _context.SaveChanges();

            Message = "✅ Продажа успешно добавлена!";
            Sale = new Sale();
            SaleDetails = new List<SaleDetail>() { new SaleDetail() };
            Employees = _context.Employees.ToList();
            Drugs = _context.Drugs.ToList();

            return Page();
        }
    }
}
