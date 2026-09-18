using BookStore.Api.Mapping;
using BookStore.Application.Services;
using BookStore.Shared.Books;

namespace BookStore.Api.Endpoints;

public static class BookEndpoints
{
  public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/api/books").WithTags("Books");

    group.MapGet("/", async (IBookService service) =>
    {
      var books = await service.GetAllAsync();
      return Results.Ok(books.Select(b => b.ToResponse()));
    });

    group.MapGet("/{id:int}", async (int id, IBookService service) =>
    {
      var book = await service.GetByIdAsync(id);
      return book is null ? Results.NotFound() : Results.Ok(book.ToResponse());
    });

    group.MapPost("/", async (BookRequest request, IBookService service) =>
    {
      try
      {
        var created = await service.CreateAsync(request.ToEntity());
        return Results.Created($"/api/books/{created.Id}", created.ToResponse());
      }
      catch (InvalidOperationException ex)
      {
        return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
      }
    });

    group.MapPut("/{id:int}", async (int id, BookRequest request, IBookService service) =>
    {
      try
      {
        var updated = await service.UpdateAsync(id, request.ToEntity());
        return updated is null ? Results.NotFound() : Results.Ok(updated.ToResponse());
      }
      catch (InvalidOperationException ex)
      {
        return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
      }
    });

    group.MapDelete("/{id:int}", async (int id, IBookService service) =>
    {
      try
      {
        var deleted = await service.DeleteAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
      }
      catch (InvalidOperationException ex)
      {
        return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
      }
    });

    return app;
  }
}
