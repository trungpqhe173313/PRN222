using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhạmQuốcTrungRazorPages.Entities;

namespace PhạmQuốcTrungRazorPages.Pages.AccountPage
{
    public class IndexModel : PageModel
    {
        private readonly PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext _context;

        public IndexModel(PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext context)
        {
            _context = context;
        }

        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var role = HttpContext.Session.GetInt32("UserRole");

            if (role == null || role != 0) // 1 là quyền yêu cầu
            {
                 RedirectToPage("/Home/Unauthorized");
            }

            SystemAccount = await _context.SystemAccounts.ToListAsync();
        }
    }
}
