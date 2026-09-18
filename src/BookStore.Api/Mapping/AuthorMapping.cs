using BookStore.Domain.Entities;
using BookStore.Shared.Authors;

namespace BookStore.Api.Mapping;

public static class AuthorMapping
{
  public static AuthorResponse ToResponse(this Author author) => new()
  {
    Id = author.Id,
    Name = author.Name,
    Bio = author.Bio,
    BirthDate = author.BirthDate,
    Nationality = author.Nationality,
    BookCount = author.Books?.Count ?? 0
  };

  public static Author ToEntity(this AuthorRequest request) => new()
  {
    Name = request.Name,
    Bio = request.Bio,
    BirthDate = request.BirthDate,
    Nationality = request.Nationality
  };
}
