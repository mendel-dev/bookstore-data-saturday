namespace BookStore.Shared.Books;

public class BookRequest
{
  public string Title { get; set; } = string.Empty;
  public string Isbn { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string Genre { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int Stock { get; set; }
  public DateTime PublishedDate { get; set; }
  public int AuthorId { get; set; }
}
