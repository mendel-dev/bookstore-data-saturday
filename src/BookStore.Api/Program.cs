using BookStore.Api.Endpoints;
using BookStore.Application.Repositories;
using BookStore.Application.Services;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
  options.AddPolicy("WebClient", policy =>
  {
    policy.WithOrigins(allowedOrigins)
          .AllowAnyHeader()
          .AllowAnyMethod();
  });
});

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseInMemoryDatabase("BookStoreDb"));

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
  DataSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseCors("WebClient");

app.MapAuthorEndpoints();
app.MapBookEndpoints();

app.Run();
