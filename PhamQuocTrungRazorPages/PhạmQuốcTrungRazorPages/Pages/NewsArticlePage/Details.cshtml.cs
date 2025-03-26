using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhạmQuốcTrungRazorPages.Entities;

namespace PhạmQuốcTrungRazorPages.Pages.NewsArticlePage
{
    public class DetailsModel : PageModel
    {
        private readonly PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext _context;

        public DetailsModel(PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext context)
        {
            _context = context;
        }

        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = await _context.NewsArticles.FirstOrDefaultAsync(m => m.NewsArticleId == id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }
    }
}
