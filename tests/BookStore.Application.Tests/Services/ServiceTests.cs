using BookStore.Application.Repositories;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Xunit;

namespace BookStore.Application.Tests.Services;

public class ServiceTests
{
  [Fact]
  public async Task CreateAuthorAsync_NormalizesTextAndPersistsAuthor()
  {
    var authors = new FakeAuthorRepository();
    var service = new AuthorService(authors);
    var author = new Author
    {
      Name = "  Octavia Butler  ",
      Nationality = "  American  ",
      Bio = "  Science fiction author.  ",
      BirthDate = new DateTime(1947, 6, 22)
    };

    var created = await service.CreateAsync(author);

    Assert.Equal("Octavia Butler", created.Name);
    Assert.Equal("American", created.Nationality);
    Assert.Equal("Science fiction author.", created.Bio);
    Assert.Single(authors.Authors);
  }

  [Fact]
  public async Task DeleteAuthorAsync_WithBooks_ThrowsAndDoesNotRemoveAuthor()
  {
    var author = new Author { Id = 1, Name = "Author" };
    author.Books.Add(new Book { Id = 1, Title = "Book" });
    var authors = new FakeAuthorRepository(author);
    var service = new AuthorService(authors);

    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(1));

    Assert.Equal("Cannot delete an author who still has books.", exception.Message);
    Assert.Single(authors.Authors);
  }

  [Fact]
  public async Task CreateBookAsync_NormalizesTextAndSetsAvailabilityFromStock()
  {
    var authors = new FakeAuthorRepository(new Author { Id = 1, Name = "Author" });
    var books = new FakeBookRepository();
    var service = new BookService(books, authors);
    var book = new Book
    {
      Title = "  Kindred  ",
      Isbn = "  9780807083697  ",
      Genre = "  Science Fiction  ",
      Description = "  A time-travel novel.  ",
      Price = 12.50m,
      Stock = 0,
      PublishedDate = new DateTime(1979, 6, 1),
      AuthorId = 1
    };

    var created = await service.CreateAsync(book);

    Assert.Equal("Kindred", created.Title);
    Assert.Equal("9780807083697", created.Isbn);
    Assert.Equal("Science Fiction", created.Genre);
    Assert.Equal("A time-travel novel.", created.Description);
    Assert.False(created.IsAvailable);
    Assert.Single(books.Books);
  }

  [Fact]
  public async Task CreateBookAsync_WithUnknownAuthor_ThrowsAndDoesNotPersistBook()
  {
    var service = new BookService(new FakeBookRepository(), new FakeAuthorRepository());
    var book = new Book
    {
      Title = "Book",
      Price = 10m,
      Stock = 1,
      PublishedDate = DateTime.UtcNow.AddDays(-1),
      AuthorId = 99
    };

    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateAsync(book));

    Assert.Equal("Author does not exist.", exception.Message);
  }

  [Fact]
  public async Task UpdateBookAsync_WithUnknownId_ReturnsNull()
  {
    var service = new BookService(new FakeBookRepository(), new FakeAuthorRepository());

    var result = await service.UpdateAsync(99, new Book());

    Assert.Null(result);
  }

  private sealed class FakeAuthorRepository : IAuthorRepository
  {
    public List<Author> Authors { get; } = [];

    public FakeAuthorRepository(params Author[] authors)
    {
      Authors.AddRange(authors);
    }

    public Task<List<Author>> GetAllWithBooksAsync() => Task.FromResult(Authors);
    public Task<Author?> GetByIdWithBooksAsync(int id) => Task.FromResult(Authors.FirstOrDefault(a => a.Id == id));
    public Task<Author?> GetByIdAsync(int id) => Task.FromResult(Authors.FirstOrDefault(a => a.Id == id));
    public Task<bool> ExistsAsync(int id) => Task.FromResult(Authors.Any(a => a.Id == id));

    public Task AddAsync(Author author)
    {
      Authors.Add(author);
      return Task.CompletedTask;
    }

    public void Remove(Author author) => Authors.Remove(author);
    public Task SaveChangesAsync() => Task.CompletedTask;
  }

  private sealed class FakeBookRepository : IBookRepository
  {
    public List<Book> Books { get; } = [];

    public Task<List<Book>> GetAllWithAuthorAsync() => Task.FromResult(Books);
    public Task<Book?> GetByIdWithAuthorAsync(int id) => Task.FromResult(Books.FirstOrDefault(b => b.Id == id));
    public Task<Book?> GetByIdAsync(int id) => Task.FromResult(Books.FirstOrDefault(b => b.Id == id));

    public Task AddAsync(Book book)
    {
      Books.Add(book);
      return Task.CompletedTask;
    }

    public void Remove(Book book) => Books.Remove(book);
    public Task SaveChangesAsync() => Task.CompletedTask;
  }
}
