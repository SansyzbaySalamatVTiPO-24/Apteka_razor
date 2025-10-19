using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Apteka_razor.Pages
{
    public class DrugsModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<Drug> Drugs { get; set; } = new();

        public DrugsModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Drugs = _context.Drugs.ToList();
        }
    }
}
