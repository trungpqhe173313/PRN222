using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhạmQuốcTrungRazorPages.Entities;

namespace PhạmQuốcTrungRazorPages.Pages.AccountPage
{
    public class CreateModel : PageModel
    {
        private readonly PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext _context;

        public CreateModel(PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SystemAccounts.Add(SystemAccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
