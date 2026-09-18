using BookStore.Application.Repositories;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
  private readonly BookStoreContext _db;

  public AuthorRepository(BookStoreContext db)
  {
    _db = db;
  }

  public async Task<List<Author>> GetAllWithBooksAsync()
  {
    return await _db.Authors
        .Include(a => a.Books)
        .OrderBy(a => a.Name)
        .ToListAsync();
  }

  public async Task<Author?> GetByIdWithBooksAsync(int id)
  {
    return await _db.Authors
        .Include(a => a.Books)
        .FirstOrDefaultAsync(a => a.Id == id);
  }

  public async Task<Author?> GetByIdAsync(int id)
  {
    return await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
  }

  public async Task<bool> ExistsAsync(int id)
  {
    return await _db.Authors.AnyAsync(a => a.Id == id);
  }

  public async Task AddAsync(Author author)
  {
    await _db.Authors.AddAsync(author);
  }

  public void Remove(Author author)
  {
    _db.Authors.Remove(author);
  }

  public Task SaveChangesAsync()
  {
    return _db.SaveChangesAsync();
  }
}
