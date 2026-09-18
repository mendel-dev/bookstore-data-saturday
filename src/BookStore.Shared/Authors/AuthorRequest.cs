using System.ComponentModel.DataAnnotations;

namespace BookStore.Shared.Authors;

public class AuthorRequest
{
  [Required(ErrorMessage = "Author name is required.")]
  [StringLength(150, ErrorMessage = "Author name must be at most 150 characters.")]
  public string Name { get; set; } = string.Empty;
  public string Bio { get; set; } = string.Empty;
  public DateTime BirthDate { get; set; }
  public string Nationality { get; set; } = string.Empty;
}
