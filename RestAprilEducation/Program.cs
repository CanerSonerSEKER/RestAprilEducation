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
builder.Services.AddScoped<IProductRepository, ProductRepositoryWithInMemory>();

builder.Services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>();
builder.Services.AddVersioningExt();

// Register repository implementations from Persistence assembly
//builder.Services.AddPersistenceRepositories();

builder.Services.AddExceptionHandler<UserFriendlyExceptionHandler>()
    .AddExceptionHandler<BusinessExceptionHandler>()
    .AddExceptionHandler<GlobalExceptionHandler>();


var app = builder.Build();

app.MapDefaultEndpoints();

app.UseExceptionHandler((options) => 
{
});

app.AddProductEndpoints(app.AddApiVersionSetExt());
app.AddVersionExampleEndpoint(app.AddApiVersionSetExt());

app.AddExceptionHandlerExampleEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
