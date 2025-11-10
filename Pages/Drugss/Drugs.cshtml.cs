using Microsoft.AspNetCore.Mvc.RazorPages;
using Apteka_razor.Data;
using Apteka_razor.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Apteka_razor.Pages.Drugs
{
    public class DrugsModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<Drug> DrugList { get; set; } = new();

        public DrugsModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            DrugList = _context.Drugs.ToList();
        }
    }
}
