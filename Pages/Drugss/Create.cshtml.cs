using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Apteka_razor.Pages.Drugs
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Drug Drug { get; set; } = new Drug();

        public SelectList ManufacturersList { get; set; } = null!;
        public SelectList CategoriesList { get; set; } = null!;
        public SelectList PharmaciesList { get; set; } = null!;

        public void OnGet()
        {
            ManufacturersList = new SelectList(new List<string>
            {
                "Bayer",
                "KRKA",
                "Pfizer",
                "Gedeon Richter",
                "GlaxoSmithKline"
            });

            CategoriesList = new SelectList(new List<string>
            {
                "Таблетки 30 мг",
                "Таблетки 50 мг",
                "Капсулы 500 мг",
                "Сироп 100 мл",
                "Мазь 20 г",
                "Капли 10 мл",
                "Порошок 1 г"
            });

            // 🔹 Подгружаем список аптек
            PharmaciesList = new SelectList(
                _context.Pharmacies
                        .Select(p => new { p.Id, Display = $"{p.Name} — {p.Address}" })
                        .ToList(),
                "Id",
                "Display"
            );
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Drugs.Add(Drug);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Drugss/Drugs");
        }
    }
}
