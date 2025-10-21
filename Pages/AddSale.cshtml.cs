// Pages/AddSaleModel.cshtml.cs
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Валидация сотрудника
                var selectedEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Id == Sale.EmployeeId);

                if (selectedEmployee == null)
                {
                    ModelState.AddModelError("Sale.EmployeeId", "Выбранный сотрудник не существует");
                    return Page();
                }

                // Заполняем обязательные поля согласно структуре БД
                if (Sale.SaleDate == default)
                    Sale.SaleDate = DateTime.Today;
                Sale.CustomerId = 1; // Временное значение, т.к. поле NOT NULL в БД
                Sale.Total = 0;      // Временное значение, т.к. поле NOT NULL в БД
                Sale.TotalPrice = 0; // Временное значение

                _context.Sales.Add(Sale);
                await _context.SaveChangesAsync();

                // Сохраняем детали продажи
                decimal totalSalePrice = 0;
                foreach (var detailVm in SaleDetails)
                {
                    if (detailVm.DrugId > 0 && detailVm.Quantity > 0)
                    {
                        var drug = await _context.Drugs.FindAsync(detailVm.DrugId);
                        if (drug != null)
                        {
                            var detail = new SaleDetail
                            {
                                SaleId = Sale.Id,
                                DrugId = detailVm.DrugId,
                                Quantity = detailVm.Quantity,
                                UnitPrice = drug.Price,
                                TotalPrice = detailVm.Quantity * drug.Price
                            };

                            totalSalePrice += detail.TotalPrice;
                            _context.SaleDetails.Add(detail);
                        }
                    }
                }

                // Обновляем общую сумму продажи
                Sale.TotalPrice = totalSalePrice;
                Sale.Total = (double)totalSalePrice; // Конвертируем в double для поля Total
                await _context.SaveChangesAsync();

                Message = "Продажа успешно добавлена!";

                // Очищаем форму
                Sale = new Sale();
                SaleDetails = new List<SaleDetailViewModel> { new SaleDetailViewModel() };
                ModelState.Clear();

                await LoadData();
            }
            catch (Exception ex)
            {
                Message = $"Ошибка: {ex.Message}";
                if (ex.InnerException != null)
                {
                    Message += $" | Детали: {ex.InnerException.Message}";
                }
                await LoadData();
            }

            return Page();
        }

        private async Task LoadData()
        {
            try
            {
                Employees = await _context.Employees
                    .Include(e => e.Pharmacy)
                    .ToListAsync();

                Drugs = await _context.Drugs.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки данных: {ex.Message}");
                Employees = new List<Employee>();
                Drugs = new List<Drug>();
            }
        }
    }

    public class SaleDetailViewModel
    {
        public int DrugId { get; set; }
        public int Quantity { get; set; }
    }
}