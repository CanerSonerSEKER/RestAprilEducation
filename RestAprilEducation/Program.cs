var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
