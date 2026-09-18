using BookStore.Domain.Entities;
using BookStore.Shared.Books;

namespace BookStore.Api.Mapping;

public static class BookMapping
{
  public static BookResponse ToResponse(this Book book) => new()
  {
    Id = book.Id,
    Title = book.Title,
    Isbn = book.Isbn,
    Description = book.Description,
    Genre = book.Genre,
    Price = book.Price,
    Stock = book.Stock,
    PublishedDate = book.PublishedDate,
    IsAvailable = book.IsAvailable,
    AuthorId = book.AuthorId,
    AuthorName = book.Author?.Name ?? string.Empty
  };

  public static Book ToEntity(this BookRequest request) => new()
  {
    Title = request.Title,
    Isbn = request.Isbn,
    Description = request.Description,
    Genre = request.Genre,
    Price = request.Price,
    Stock = request.Stock,
    PublishedDate = request.PublishedDate,
    AuthorId = request.AuthorId
  };
}
