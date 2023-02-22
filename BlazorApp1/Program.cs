using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using BlazorApp1.Models;
using BlazorApp1.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient(".NetApi",
    configureClient => configureClient.BaseAddress = new Uri(builder.Configuration[".NetApi"]));
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
        options.Scope = "openid profile offline_access email";
        options.OpenIdConnectEvents = new OpenIdConnectEvents
        {
            OnTokenValidated = (context) =>
            {
                var token = context.TokenEndpointResponse?.IdToken;
                var claims = new List<Claim>
                    {
                        new("jwt_token", token!)
                    };

                var appIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                context.Principal?.AddIdentity(appIdentity);
                context.Response.Cookies.Append("auth_token", token!, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Task.CompletedTask;
            },
            OnTicketReceived = _ =>
            {
                Console.WriteLine("Authentication ticket received.");
                return Task.FromResult(true);
            },

            OnAuthorizationCodeReceived = _ =>
            {
                Console.WriteLine("Authorization code received");
                return Task.FromResult(true);
            },
            OnUserInformationReceived = _ =>
            {
                Console.WriteLine("Token validation received");
                return Task.FromResult(true);
            }
        };

    }).WithAccessToken(options =>
    {

        options.Audience = builder.Configuration["Auth0:Audience"];
        options.UseRefreshTokens = true;
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("NoRolePolicy", policy => policy.RequireAssertion(context => !context.User.HasClaim(c => c.Type == ClaimTypes.Role)));
});

builder.Services.AddSingleton<IGetUserIdAsync, GetIdUsersFromToken>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IDiscoveryCache>(sp =>
{
	var factory = sp.GetRequiredService<IHttpClientFactory>();
	return new DiscoveryCache(
		"https://demo.identityserver.io/",
		() => factory.CreateClient());
});

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddSingleton<Token>();
builder.Services.AddSingleton<Authentication>();
builder.Services.AddHttpClient();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();