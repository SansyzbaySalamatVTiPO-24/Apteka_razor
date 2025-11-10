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

            // Проверка роли пользователя
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && role != "Pharmacist" && role != "Seller")
            {
                Message = "У вас нет прав для добавления продаж.";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Model error: " + error.ErrorMessage);
                }
                Message = "❌ Ошибка модели. Проверьте поля формы.";
                return Page();
            }

            try
            {
                // 🔹 Получаем ID вошедшего сотрудника из сессии
                var employeeId = HttpContext.Session.GetInt32("EmployeeId");
                if (employeeId == null)
                {
                    Message = "Ошибка: не удалось определить сотрудника. Авторизуйтесь заново.";
                    return Page();
                }

                Sale.EmployeeId = employeeId.Value;


                decimal totalSalePrice = 0;
                var saleDetails = new List<SaleDetail>();

                // 🔹 Формируем детали продажи
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

                            // Уменьшаем количество товара
                            drug.Quantity -= detailVm.Quantity;
                        }
                    }
                }

                // 🔹 Итоговая сумма продажи
                Sale.TotalPrice = totalSalePrice;

                // 🔹 Добавляем и сохраняем продажу
                _context.Sales.Add(Sale);
                await _context.SaveChangesAsync();

                // 🔹 Привязываем детали к продаже
                foreach (var detail in saleDetails)
                    detail.SaleId = Sale.Id;

                _context.SaleDetails.AddRange(saleDetails);
                await _context.SaveChangesAsync();

                Message = "✅ Продажа успешно добавлена!";

                // 🔹 Сброс формы
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
