using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Supplied database connection due to the fact that
// we created this web app to use Individual Accounts
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add another GetConnectionString to reference our database
// connectionString
var connectionStringChinook = builder.Configuration.GetConnectionString("ChinookDB");

// Given for the DB connection to DefaultConnection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Code the DBConnection to the application DB Context for Chinook
// The implementation of he connect AND registration of the
// ChinookSystem services will be done in the ChinookSystem
// class library.
// So to accomplish this task, we will be using an "extension" method
// The extension method will extend the IServiceCollection Class.
// The extension method requires a parameter options.UseSqlServer(XXX)
// where XXX is the connection string variable
// builder.Services.ChinookSystemBackendDependencies(options => options.UseSqlServer(connectionStringChinook));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
