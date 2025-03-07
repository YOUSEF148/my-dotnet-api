using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Repositories;
using Core.Interface;
using Repository.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using core.Interface;
using Service;
using Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add Database Context (Change connection string as needed)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));

// 🔹 Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 🔹 Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();

// 🔹 Register Services
builder.Services.AddScoped< JobService>();  // ✅ Fixed missing interface
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

// 🔹 Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lfkjdflqevn3434nvejrekjtrjevnhjrehvntljwehtvjqhtljqhtjhqlthqlthek"))
			};
		});

builder.Services.AddLogging();

//builder.Services.AddScoped<IApplicationService, ApplicationService>();

//builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// 🔹 Enable Authorization

builder.Services.AddScoped<IJobRepository, JobRepository>();

// 🔹 Add Controllers
builder.Services.AddControllers();

// 🔹 Add Swagger with Authorization Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Job Portal API",
		Version = "v1",
		Description = "API for managing jobs and users in a job portal."
	});

	// 🔹 Add JWT Authorization in Swagger UI
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter 'Bearer' followed by a space and your JWT token."
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});
builder.Services.AddAuthorization();
// 🔹 Build & Configure Middleware Pipeline
var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");

// 🔹 Middleware Configuration
app.UseHttpsRedirection();
app.UseAuthentication();  // ✅ Ensures authentication middleware is applied
app.UseAuthorization();
app.MapControllers();

// 🔹 Run the App
app.Run();
