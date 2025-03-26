using PhạmQuốcTrungRazorPages.Repositories.Interfaces;
using PhạmQuốcTrungRazorPages.Repositories;
using PhạmQuốcTrungRazorPages.Services.Interfaces;
using PhạmQuốcTrungRazorPages.Services;
using Microsoft.EntityFrameworkCore;
using PhạmQuốcTrungRazorPages.Entities;
using SignalRCrud.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<CommentService>();
builder.Services.AddSingleton<ISystemAccountRepository>(sp =>
{
    var context = sp.GetRequiredService<FUNewsDbContext>();
    return SystemAccountRepository.Instance(context);
});
builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddRazorPages(); // hoặc AddControllersWithViews()
builder.Services.AddSignalR();
builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout session (30 phút)
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("FUNewsManagement");

// Đăng ký DbContext với DI Container
builder.Services.AddDbContext<FUNewsDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapHub<SignalrServer>("/signalRServer");
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
