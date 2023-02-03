global using UserGuide.Shared.Models;
using Microsoft.EntityFrameworkCore;
using UserGuide.Core.Repository;
using UserGuide.Server.Data;
using UserGuide.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(config =>
{
    config.UseInMemoryDatabase("MEMORY");
});
//builder.Services.AddDbContext<ApplicationDBContext>(config =>
//{
//    config.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IActiveDirectoryService, ActiveDirectoryStub>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
