namespace BookStore.Shared.Authors;

public class AuthorResponse
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Bio { get; set; } = string.Empty;
  public DateTime BirthDate { get; set; }
  public string Nationality { get; set; } = string.Empty;
  public int BookCount { get; set; }
}
