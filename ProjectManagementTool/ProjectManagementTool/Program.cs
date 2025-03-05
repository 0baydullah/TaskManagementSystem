using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using BusinessLogicLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("PMSDB") ?? throw new InvalidOperationException("Connection string 'PMSDbContext' not found.");
builder.Services.AddDbContext<PMSDBContext>(options =>
    options.UseSqlServer(connectionString));

// Register Dependency injection of iservice and irepository layer
builder.Services.AddServiceLayer();
builder.Services.AddRepositoryLayer();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
