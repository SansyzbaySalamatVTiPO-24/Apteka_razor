using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Apteka_razor.Pages
{
    public class SalesModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Sale> Sales { get; set; } = new();

        public SalesModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Sales = _context.Sales
                .Include(s => s.Employee)
                .ToList();
        }
    }
}
