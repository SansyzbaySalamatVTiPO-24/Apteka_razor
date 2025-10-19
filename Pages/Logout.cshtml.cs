using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apteka_razor.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Очистка всех данных сессии
            HttpContext.Session.Clear();

            // Возврат на страницу входа
            return RedirectToPage("/Login");
        }
    }
}
