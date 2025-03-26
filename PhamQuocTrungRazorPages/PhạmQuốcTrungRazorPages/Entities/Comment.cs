namespace PhạmQuốcTrungRazorPages.Entities
{
    public class Comment
    {
        public string CommentId { get; set; } = Guid.NewGuid().ToString();

        public string NewsArticleId { get; set; } = null!; // ID bài viết gắn comment

        public string UserId { get; set; } = null!; // ID người dùng gửi comment

        public string UserName { get; set; } = null!; // Tên hiển thị

        public string Content { get; set; } = null!; // Nội dung bình luận

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsEdited { get; set; } = false;
    }
}
