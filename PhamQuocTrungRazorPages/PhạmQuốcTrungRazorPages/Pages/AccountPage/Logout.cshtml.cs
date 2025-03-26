using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhạmQuốcTrungRazorPages.Pages.AccountPage
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            Console.WriteLine("Logout request received.");
            HttpContext.Session.Clear();
            return RedirectToPage("/AccountPage/Login");
        }
    }
}
