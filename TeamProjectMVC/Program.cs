using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamProjectMVC.Data;
using TeamProjectMVC.Entity;
<<<<<<< HEAD
//using TeamProjectMVC.Repository.Impl;
//using TeamProjectMVC.Repository;
using TeamProjectMVC.Services;
=======
using TeamProjectMVC.Repository.Impl;
using TeamProjectMVC.Repository;
using TeamProjectMVC.Service;
>>>>>>> 5d627eff87eae85fb44e50a31c2a65bc396665a3

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

<<<<<<< HEAD
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<AuditLogService>();
=======
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<AccountService>();
>>>>>>> 5d627eff87eae85fb44e50a31c2a65bc396665a3

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnectionStrings");


builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.EnableSensitiveDataLogging();
});



var app = builder.Build();

using (var onescope = app.Services.CreateScope())
{ 
    var host = app;
    await Seed.SeedUsersAndRolesAsync(host);
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

app.Run();
