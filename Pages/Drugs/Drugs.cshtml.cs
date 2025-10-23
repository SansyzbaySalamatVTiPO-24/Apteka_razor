using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data;
using Apteka_razor.Data.Models;

namespace Apteka_razor.Pages.Drugs
{
    public class DrugsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DrugsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Drug> DrugList { get; set; } = new();

        public async Task OnGetAsync()
        {
            DrugList = await _context.Drugs.ToListAsync();
        }
    }
}
