using GoodLawSoftware.Application;
using GoodLawSoftware.Application.IRepositoies;
using GoodLawSoftware.Application.Service.UserService;
using GoodLawSoftware.Infrastructure;
using GoodLawSoftware.LoginApi.Helper;
using GoodLawSoftware.LoginApi.Service;
using GoodLawSoftware.Service;
using JWT_NET_5.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#region Config SeriLog
	var logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.Enrich.FromLogContext()
	.CreateLogger();
	builder.Logging.ClearProviders();
	builder.Logging.AddSerilog(logger);
#endregion
// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddDbContext<GoodLawContext>(opt=>
			opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<GoodLawContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserService,UserService>();

#region JWT Config
	builder.Services.AddAuthentication(options =>
	{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
				   .AddJwtBearer(o =>
				   {
					   o.RequireHttpsMetadata = false;
					   o.SaveToken = false;
					   o.TokenValidationParameters = new TokenValidationParameters
					   {
						   ValidateIssuerSigningKey = true,
						   ValidateIssuer = true,
						   ValidateAudience = false,
						   ValidateLifetime = true,
						   ValidIssuer = builder.Configuration["JWT:Issuer"],
						   ValidAudience = builder.Configuration["JWT:Audience"],
						   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
					   };
				   }); 
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
