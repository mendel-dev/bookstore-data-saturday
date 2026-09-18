using BookStore.Api.Mapping;
using BookStore.Application.Services;
using BookStore.Shared.Authors;

namespace BookStore.Api.Endpoints;

public static class AuthorEndpoints
{
  public static IEndpointRouteBuilder MapAuthorEndpoints(this IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("/api/authors").WithTags("Authors");

    group.MapGet("/", async (IAuthorService service) =>
    {
      var authors = await service.GetAllAsync();
      return Results.Ok(authors.Select(a => a.ToResponse()));
    });

    group.MapGet("/{id:int}", async (int id, IAuthorService service) =>
    {
      var author = await service.GetByIdAsync(id);
      return author is null ? Results.NotFound() : Results.Ok(author.ToResponse());
    });

    group.MapPost("/", async (AuthorRequest request, IAuthorService service) =>
    {
      try
      {
        var created = await service.CreateAsync(request.ToEntity());
        return Results.Created($"/api/authors/{created.Id}", created.ToResponse());
      }
      catch (InvalidOperationException ex)
      {
        return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
      }
    });

    group.MapPut("/{id:int}", async (int id, AuthorRequest request, IAuthorService service) =>
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

    group.MapDelete("/{id:int}", async (int id, IAuthorService service) =>
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
