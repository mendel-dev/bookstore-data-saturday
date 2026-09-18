using BookStore.Application.Repositories;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
  private readonly BookStoreContext _db;

  public BookRepository(BookStoreContext db)
  {
    _db = db;
  }

  public async Task<List<Book>> GetAllWithAuthorAsync()
  {
    return await _db.Books
        .Include(b => b.Author)
        .OrderBy(b => b.Title)
        .ToListAsync();
  }

  public async Task<Book?> GetByIdWithAuthorAsync(int id)
  {
    return await _db.Books
        .Include(b => b.Author)
        .FirstOrDefaultAsync(b => b.Id == id);
  }

  public async Task<Book?> GetByIdAsync(int id)
  {
    return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
  }

  public async Task AddAsync(Book book)
  {
    await _db.Books.AddAsync(book);
  }

  public void Remove(Book book)
  {
    _db.Books.Remove(book);
  }

  public Task SaveChangesAsync()
  {
    return _db.SaveChangesAsync();
  }
}
