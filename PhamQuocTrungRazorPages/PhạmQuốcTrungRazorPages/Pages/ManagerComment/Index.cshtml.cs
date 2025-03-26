using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhạmQuốcTrungRazorPages.Entities;
using PhạmQuốcTrungRazorPages.Services;

namespace PhạmQuốcTrungRazorPages.Pages.ManagerComment
{
    public class IndexModel : PageModel
    {
        private readonly CommentService _commentService;

        public IndexModel(CommentService commentService)
        {
            _commentService = commentService;
        }

        [BindProperty(SupportsGet = true)]
        public string NewsArticleId { get; set; }

        public List<Comment> Comments { get; set; }

        // Hàm xử lý khi trang được tải (GET request)
        public void OnGet(int ?id)
        {
            //NewsArticleId = id.ToString();
            // Lấy tất cả bình luận của bài báo với ID tương ứng
            Comments = _commentService.GetCommentsByArticle(NewsArticleId);
        }
    }
}
