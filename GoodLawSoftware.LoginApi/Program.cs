using GoodLawSoftware.Application;
using GoodLawSoftware.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
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
builder.Services.AddDbContext<GoodLawContext>(opt=>
			opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<GoodLawContext>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
