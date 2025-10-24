using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using Microsoft.AspNetCore.Authorization;
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
