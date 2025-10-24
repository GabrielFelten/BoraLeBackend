using BoraLe.Application.Interfaces;
using BoraLe.Application.Services;
using BoraLe.Domain.Interfaces;
using BoraLe.Infrastructure.Config;
using BoraLe.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração Firebase
builder.Services.AddSingleton<FirebaseConfig>();

// Registrando repositório e serviço
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
