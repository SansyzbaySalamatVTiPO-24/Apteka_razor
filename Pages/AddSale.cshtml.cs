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

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Drug> Drugs { get; set; } = new List<Drug>();

        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await LoadData();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadData();

            // проверка роли пользователя
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && role != "Pharmacist" && role != "Seller")
            {
                Message = "У вас нет прав для добавления продаж.";
                return Page();
            }

            if (!ModelState.IsValid)
                return Page();

            try
            {
                var selectedEmployee = await _context.Employees.FindAsync(Sale.EmployeeId);
                if (selectedEmployee == null)
                {
                    ModelState.AddModelError("Sale.EmployeeId", "Выбранный сотрудник не существует");
                    return Page();
                }

                if (Sale.SaleDate == default)
                    Sale.SaleDate = DateTime.Now;

                Sale.CustomerId = 1; // временно

                decimal totalSalePrice = 0;
                var saleDetails = new List<SaleDetail>();

                // формируем детали продажи
                foreach (var detailVm in SaleDetails)
                {
                    if (detailVm.DrugId > 0 && detailVm.Quantity > 0)
                    {
                        var drug = await _context.Drugs.FindAsync(detailVm.DrugId);
                        if (drug != null)
                        {
                            if (drug.Quantity < detailVm.Quantity)
                            {
                                ModelState.AddModelError("", $"Недостаточно товара: {drug.Name}");
                                return Page();
                            }

                            var price = drug.Price ?? 0m;
                            var detail = new SaleDetail
                            {
                                DrugId = detailVm.DrugId,
                                Quantity = detailVm.Quantity,
                                Price = price
                            };

                            totalSalePrice += price * detailVm.Quantity;
                            saleDetails.Add(detail);

                            // уменьшаем количество товара на складе
                            drug.Quantity -= detailVm.Quantity;
                        }
                    }
                }

                Sale.TotalPrice = totalSalePrice;

                _context.Sales.Add(Sale);
                await _context.SaveChangesAsync(); // сохраняем, чтобы получить Sale.Id

                foreach (var detail in saleDetails)
                    detail.SaleId = Sale.Id;

                _context.SaleDetails.AddRange(saleDetails);
                await _context.SaveChangesAsync();

                Message = "✅ Продажа успешно добавлена!";

                // сброс формы
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
