using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhạmQuốcTrungRazorPages.Entities;
using PhạmQuốcTrungRazorPages.Services;

namespace PhạmQuốcTrungRazorPages.Pages.ManagerComment
{
    public class DeleteModel : PageModel
    {
        private readonly CommentService _commentService;

        public DeleteModel(CommentService commentService)
        {
            _commentService = commentService;
        }

        [BindProperty(SupportsGet = true)]
        public string CommentId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NewsArticleId { get; set; }

        public Comment Comment { get; set; }

        public void OnGet()
        {
            Comment = _commentService.GetCommentsByArticle(NewsArticleId)
                .FirstOrDefault(c => c.CommentId == CommentId);
        }

        public IActionResult OnPost()
        {
            var success = _commentService.DeleteComment(NewsArticleId, CommentId, "user01");

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("./Comments", new { articleId = NewsArticleId });
        }
    }
}
