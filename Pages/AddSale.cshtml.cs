using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data;
using Apteka_razor.Data.Models;

namespace Apteka_razor.Pages
{
    public class AddSaleModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddSaleModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sale Sale { get; set; } = new Sale();

        [BindProperty]
        public List<SaleDetailViewModel> SaleDetails { get; set; } = new List<SaleDetailViewModel>
        {
            new SaleDetailViewModel()
        };

        public List<Employee> Employees { get; set; } = new();
        public List<Drug> Drugs { get; set; } = new();
        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await LoadData();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadData();

            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && role != "Pharmacist" && role != "Seller")
            {
                Message = "У вас нет прав для добавления продаж.";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                Message = "❌ Ошибка модели. Проверьте поля.";
                return Page();
            }

            try
            {
                var employeeId = HttpContext.Session.GetInt32("EmployeeId");
                if (employeeId == null)
                {
                    Message = "Ошибка: сотрудник не определён. Перезайдите в систему.";
                    return Page();
                }

                Sale.EmployeeId = employeeId.Value;

                decimal totalSalePrice = 0;
                var saleDetails = new List<SaleDetail>();

                foreach (var detailVm in SaleDetails)
                {
                    if (detailVm.DrugId <= 0 || detailVm.Quantity <= 0)
                        continue;

                    var drug = await _context.Drugs.FindAsync(detailVm.DrugId);

                    if (drug == null)
                        continue;

                    if (drug.Quantity < detailVm.Quantity)
                    {
                        ModelState.AddModelError("", $"Недостаточно товара: {drug.Name}");
                        return Page();
                    }

                    var price = drug.Price ?? 0m;

                    saleDetails.Add(new SaleDetail
                    {
                        DrugId = detailVm.DrugId,
                        Quantity = detailVm.Quantity,
                        Price = price
                    });

                    totalSalePrice += price * detailVm.Quantity;

                    drug.Quantity -= detailVm.Quantity;
                }

                Sale.TotalPrice = totalSalePrice;

                _context.Sales.Add(Sale);
                await _context.SaveChangesAsync();

                foreach (var detail in saleDetails)
                    detail.SaleId = Sale.Id;

                _context.SaleDetails.AddRange(saleDetails);
                await _context.SaveChangesAsync();

                Message = "✅ Продажа успешно добавлена!";
                Sale = new Sale();
                SaleDetails = new List<SaleDetailViewModel> { new SaleDetailViewModel() };
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                Message = $"Ошибка: {ex.Message}";
                if (ex.InnerException != null)
                    Message += $" | Детали: {ex.InnerException.Message}";
            }

            return Page();
        }

        private async Task LoadData()
        {
            Employees = await _context.Employees.Include(e => e.Pharmacy).ToListAsync();
            Drugs = await _context.Drugs.ToListAsync();
        }
    }

    public class SaleDetailViewModel
    {
        public int DrugId { get; set; }
        public int Quantity { get; set; }
    }
}
