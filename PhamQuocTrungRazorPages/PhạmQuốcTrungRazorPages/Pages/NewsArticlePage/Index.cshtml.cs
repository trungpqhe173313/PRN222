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
    public class IndexModel : PageModel
    {
        private readonly PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext _context;

        public IndexModel(PhạmQuốcTrungRazorPages.Entities.FUNewsDbContext context)
        {
            _context = context;
        }

        public IList<NewsArticle> NewsArticle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            NewsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy).ToListAsync();
        }
    }
}
