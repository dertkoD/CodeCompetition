using BackEndCompetition.Authentication;
using BackEndCompetition.Services.Logging;
using CompetitionLibrary.Models;
using CompetitionLibrary.Repositories;
using CompetitionLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbCompetContext>(option =>
	option.UseSqlServer(builder.Configuration.GetConnectionString("ContextDb")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
	options.Audience = builder.Configuration["Auth0:Audience"];
});
builder.Services.AddAuth0AuthenticationClient(config =>
{
	config.Domain = builder.Configuration["Auth0:Domain"];
	config.ClientId = builder.Configuration["ManagementAuth0:ClientId"];
	config.ClientSecret = builder.Configuration["ManagementAuth0:ClientSecret"];
});
builder.Services.AddAuth0ManagementClient().AddManagementAccessToken();
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("read:user", policy => policy.Requirements.Add(new HasScopeRequirement("read:user", $"https://{builder.Configuration["Auth0:Domain"]}/")));
	options.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement("read:users", $"https://{builder.Configuration["Auth0:Domain"]}/")));
});

builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.log"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<UserHelp>(); 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MapConfig));

builder.Services.AddSwaggerGen(opt => {
	opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});
	opt.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
