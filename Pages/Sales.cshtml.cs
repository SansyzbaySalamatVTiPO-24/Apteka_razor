using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data.Models;
using Apteka_razor.Data;

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
            // Берем все продажи с деталями и сотрудниками
            Sales = await _context.Sales
                .Include(s => s.Employee)
                .Include(s => s.SaleDetails)
                    .ThenInclude(d => d.Drug)
                .ToListAsync();
        }
    }
}
