using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PhạmQuốcTrungRazorPages.Entities;
using PhạmQuốcTrungRazorPages.Services;

namespace PhạmQuốcTrungRazorPages.Pages.AccountPage
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public SystemAccount account  { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(account.AccountEmail) || string.IsNullOrEmpty(account.AccountPassword))
            {
                ErrorMessage = "Email và mật khẩu không được để trống!";
                return Page();
            }

            var user = _authService.Authenticate(account.AccountEmail, account.AccountPassword);

            if (user == null)
            {
                ErrorMessage = "Email hoặc mật khẩu không đúng!";
                return Page();
            }

            // Lưu role vào Session
            HttpContext.Session.SetInt32("UserRole", (int)user.AccountRole);
            HttpContext.Session.SetString("UserEmail", user.AccountEmail);
            HttpContext.Session.SetString("Username", user.AccountName);
            HttpContext.Session.SetInt32("UserId", user.AccountId);
            HttpContext.Session.SetString("IsLoggedIn", "true");

            return user.AccountRole switch
            {
                0 => RedirectToPage("/AccountPage/Index"),
                2 => RedirectToPage("/Lecturer/Lecturer"),
                1 => RedirectToPage("/NewsArticlePage/Index"),
                3 => RedirectToPage("/Unauthorized"),
                _ => Page()
            };
        }
    }
}
