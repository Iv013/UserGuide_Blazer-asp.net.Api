global using UserGuide.Shared.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using UserGuide.Server.Data;
using UserGuide.Server.Services;
using WebAppUser.Repository;
using UserGuide.Server.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(config =>
{
    config.UseInMemoryDatabase("MEMORY");
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
//вместо работы сервиса работы с AD усановлена заглушка 
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
