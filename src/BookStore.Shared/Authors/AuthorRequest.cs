namespace BookStore.Shared.Authors;

public class AuthorRequest
{
  public string Name { get; set; } = string.Empty;
  public string Bio { get; set; } = string.Empty;
  public DateTime BirthDate { get; set; }
  public string Nationality { get; set; } = string.Empty;
}
