using Microsoft.EntityFrameworkCore;
using SignalRCRUDPractice;
using SignalRCRUDPractice.MiddlewareExtension;
using SignalRCRUDPractice.Models;
using SignalRCRUDPractice.TableDepenedency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
//builder.Services.AddSingleton<SignalRServer>();

//builder.Services.AddSingleton<TableDependencyClass>();
//builder.Services.AddScoped<SignalRServer>();

//builder.Services.AddScoped<TableDependencyClass>();

builder.Services.AddSingleton<SignalRServer>();
builder.Services.AddSingleton<TableDependencyClass>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseProductTableDependency(connectionString);
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<SignalRServer>("/signalrServer");

app.Run();
