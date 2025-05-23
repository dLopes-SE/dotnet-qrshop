using Carter;
using dotnet_qrshop.Abstractions.Authentication;
using dotnet_qrshop.Common.Messaging;
using dotnet_qrshop.Common.Models.Identity;
using dotnet_qrshop.Entities;
using dotnet_qrshop.Features.Identity;
using dotnet_qrshop.Persistence.DbContext;
using dotnet_qrshop.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI
builder.Services.AddOpenApi();

// Add Carter for endpoint mapping
builder.Services.AddCarter();

// Add Custom Mediator DI
builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(IQueryHandler<,>))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

// Identity
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<UserDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
  .AddEntityFrameworkStores<UserDbContext>()
  .AddDefaultTokenProviders();

// Authentication
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
  o.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
    ValidAudience = builder.Configuration["JwtSettings:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
  };
});

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();

  app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/openapi/v1.json", "Swagger")
  );
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();