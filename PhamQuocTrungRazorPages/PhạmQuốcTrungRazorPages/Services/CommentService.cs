using PhạmQuốcTrungRazorPages.Entities;
using System.Collections.Concurrent;
using System.Text.Json;

namespace PhạmQuốcTrungRazorPages.Services
{
    public class CommentService
    {
        // Đường dẫn lưu file JSON
        private static readonly string _storagePath = "comments.json";

        // Biến lưu trong RAM
        private readonly ConcurrentDictionary<string, List<Comment>> _comments;

        public CommentService()
        {
            // Khởi tạo bằng cách load từ file
            _comments = LoadComments();
        }

        private ConcurrentDictionary<string, List<Comment>> LoadComments()
        {
            try
            {
                if (!File.Exists(_storagePath))
                {
                    // Tạo file rỗng nếu chưa có
                    File.WriteAllText(_storagePath, "{}");
                }

                var json = File.ReadAllText(_storagePath);
                return JsonSerializer.Deserialize<ConcurrentDictionary<string, List<Comment>>>(json)
                       ?? new ConcurrentDictionary<string, List<Comment>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file: {ex.Message}");
            }
            return new ConcurrentDictionary<string, List<Comment>>();
        }

        private void SaveComments()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_comments, options);
                File.WriteAllText(_storagePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi ghi file: {ex.Message}");
            }
        }

        public List<Comment> GetCommentsByArticle(string articleId)
        {
            _comments.TryGetValue(articleId, out var comments);
            return comments ?? new List<Comment>();
        }

        public Comment AddComment(Comment comment)
        {
            var comments = _comments.GetOrAdd(comment.NewsArticleId, new List<Comment>());
            comments.Add(comment);
            SaveComments(); // Lưu vào file sau khi thêm
            return comment;
        }

        public bool EditComment(string articleId, string commentId, string userId, string newContent)
        {
            if (!_comments.TryGetValue(articleId, out var comments)) return false;

            var comment = comments.FirstOrDefault(c => c.CommentId == commentId && c.UserId == userId);
            if (comment == null) return false;

            comment.Content = newContent;
            comment.IsEdited = true;
            SaveComments(); // Lưu vào file sau khi sửa
            return true;
        }

        public bool DeleteComment(string articleId, string commentId, string userId)
        {
            if (!_comments.TryGetValue(articleId, out var comments)) return false;

            var comment = comments.FirstOrDefault(c => c.CommentId == commentId && c.UserId == userId);
            if (comment == null) return false;

            comments.Remove(comment);
            SaveComments(); // Lưu vào file sau khi xóa
            return true;
        }
    }
}