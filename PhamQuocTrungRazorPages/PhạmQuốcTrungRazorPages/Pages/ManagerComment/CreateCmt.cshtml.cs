using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PhạmQuốcTrungRazorPages.Entities;
using PhạmQuốcTrungRazorPages.Services;
using SignalRCrud.Hubs;

namespace PhạmQuốcTrungRazorPages.Pages.ManagerComment
{
    public class CreateCmtModel : PageModel
    {
        [BindProperty]
        public Comment Comment { get; set; }
        private readonly CommentService _commentService;
        private readonly IHubContext<SignalrServer> _signalrHub;
        public CreateCmtModel(CommentService commentService, IHubContext<SignalrServer> signalrHub)
        {
            _commentService = commentService;
            _signalrHub = signalrHub;
        }

        // Fake service nếu chưa có DB
        private static List<Comment> _comments = new();

        public IActionResult OnGet(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
            {
                return NotFound();
            }
            
            
            Comment = new Comment
            {
                NewsArticleId = articleId,
                
            };

            return Page();
        }

        public IActionResult OnPost()
        {

            var userId = HttpContext.Session.GetString("UserId");
            var userName = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
            {
                // Nếu chưa login hoặc session hết hạn
                return RedirectToPage("../AccountPage/Login");
            }
            Comment.CommentId = Guid.NewGuid().ToString();
            Comment.CreatedDate = DateTime.Now;
            Comment.UserId = "3";
            Comment.UserName = userName;
            // Thêm vào danh sách comment giả lập
            _commentService.AddComment(Comment);
            _signalrHub.Clients.All.SendAsync("LoadComment", Comment.NewsArticleId);
            // Quay lại trang ManagerComment với bài viết gốc
            return Redirect("/ManagerComment?NewsArticleId=" + Comment.NewsArticleId);
        }
    }
}
