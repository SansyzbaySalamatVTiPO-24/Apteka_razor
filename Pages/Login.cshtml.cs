using Apteka_razor.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Apteka_razor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public string Login { get; set; } = "";
        [BindProperty]
        public string Password { get; set; } = "";

        public string? Message { get; set; }

        public LoginModel(AuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var user = _authService.Authenticate(Login, Password);
            if (user == null)
            {
                Message = "Неверный логин или пароль!";
                return Page();
            }

            _httpContextAccessor.HttpContext!.Session.SetString("UserName", user.FullName);
            _httpContextAccessor.HttpContext!.Session.SetString("UserRole", user.Role);

            return RedirectToPage("/Index");
        }
    }
}
