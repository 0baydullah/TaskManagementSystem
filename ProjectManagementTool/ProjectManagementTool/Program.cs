using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Models.Entity;
using log4net.Config;
using log4net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("PMSDB") ?? throw new InvalidOperationException("Connection string 'PMSDbContext' not found.");
builder.Services.AddDbContext<PMSDBContext>(options => options.UseSqlServer(connectionString));

// Configuration Identity Services * IdentityUser,IdentityRole is the class name of user registration. It is customizable
builder.Services.AddIdentity<UserInfo, IdentityRole<int>>(
        options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 4;
        })
        .AddEntityFrameworkStores<PMSDBContext>().AddDefaultTokenProviders();


// Register Dependency injection of iservice and irepository layer
builder.Services.AddServiceLayer();
builder.Services.AddRepositoryLayer();

// Configure the Application Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});


var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());  // for logger
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));   // for logger


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
