using FluentValidation;
using RestAprilEducation.API.Endpoints.Products;
using RestAprilEducation.API.Extensions;
using RestAprilEducation.Application;
using RestAprilEducation.Application.Products;
using RestAprilEducation.Persistence;
using Scalar.AspNetCore;
using RestAprilEducation.API.Endpoints.Versioning;
using RestAprilEducation.API.ExceptionHandlers;
using RestAprilEducation.API.Endpoints;
using RestAprilEducation.API.Metrics;
using RestAprilEducation.API.Endpoints.Metrics;
using Microsoft.EntityFrameworkCore;
using RestAprilEducation.API.Endpoints.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// DI Container Framework (Library) 

// DI + IoC => DI Pattern

// 3 Yaşam Döngüsü Var 
// Singleton => Uygulama boyunca tek bir instance kullanılır. (Stateful)
// Scoped => Her istek için aynı instance kullanılır. (Stateful)
// Transient => Her istek geldiğinde yeni bir instance oluşturulur. (Stateless)

// builder.Services.AddSingleton<CalculateService>();
builder.Services.AddSingleton<ICalculateService, CalculateService>();
builder.Services.AddScoped<IProductsApplication, ProductsApplication>();
builder.Services.AddScoped<UserApplication>();
builder.Services.AddPersistenceExt(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>();
builder.Services.AddSingleton<AppMetrics>();
builder.Services.AddVersioningExt();

// Register repository implementations from Persistence assembly
//builder.Services.AddPersistenceRepositories();

builder.Services.AddExceptionHandler<UserFriendlyExceptionHandler>()
    .AddExceptionHandler<BusinessExceptionHandler>()
    .AddExceptionHandler<GlobalExceptionHandler>();

// İki tür authorization vardır => 
// Role based authorization policies
// Claims based authorization policies

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(configure => 
    {
        // Default olarak hangi authentication şemasını kullanacağımızı belirtiyoruz.
        configure.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        // Bir istek geldiğinde hangi authentication şemasını kullanarak authenticate edeceğimizi belirtiyoruz.
        configure.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!))
        };
    })
    .AddJwtBearer("branch-schema", options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!))
        };
    });

builder.Services.AddAuthorization(options => 
{
    // Role based authorization policy ekleyelim
    options.AddPolicy("editor-role-policy", configurePolicy =>
    {
        configurePolicy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);

        configurePolicy.RequireRole("editor");

    });

    // Claim based authorization policy ekleyelim
    options.AddPolicy("city-policy", configurePolicy =>
    {
        configurePolicy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);

        configurePolicy.RequireClaim("city", "Istanbul");
    });


    options.AddPolicy("branch-policy", configurePolicy =>
    {
        configurePolicy.AuthenticationSchemes.Add("branch-schema");
        configurePolicy.RequireClaim("branch-id");
    });
});



var app = builder.Build();

app.MapDefaultEndpoints();

app.UseExceptionHandler((options) => 
{
});

app.AddProductEndpoints(app.AddApiVersionSetExt());
app.AddVersionExampleEndpoint(app.AddApiVersionSetExt());

app.AddExceptionHandlerExampleEndpoint();
app.AddMetricEndpoints();
app.AddUserEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
