using Auth0.NET.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using projCompet.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<projCompetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("projCompetContext") ?? throw new InvalidOperationException("Connection string 'projCompetContext' not found.")));
var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
var services = builder.Services;

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie()
.AddAuth0(options =>
{
    options.Domain = config["Auth0:Domain"];
    options.ClientId = config["Auth0:ClientId"];
    options.ClientSecret = config["Auth0:ClientSecret"];
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
