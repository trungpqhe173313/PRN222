document.addEventListener("DOMContentLoaded", function () {
    if (typeof signalR === "undefined") {
        console.error("⚠️ SignalR chưa được load! Kiểm tra lại đường dẫn file hoặc thứ tự script.");
        return;
    }

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();

    connection.start()
        .then(() => console.log("✅ Kết nối SignalR thành công!"))
        .catch(err => console.error("❌ Lỗi kết nối SignalR:", err.toString()));

    connection.on("LoadComment", function (articleId) {
        location.href = "/ManagerComment?NewsArticleId=" + articleId;
    });
});
