using BookStore.Domain.Entities;

namespace BookStore.Application.Repositories;

public interface IAuthorRepository
{
  Task<List<Author>> GetAllWithBooksAsync();
  Task<Author?> GetByIdWithBooksAsync(int id);
  Task<Author?> GetByIdAsync(int id);
  Task<bool> ExistsAsync(int id);
  Task AddAsync(Author author);
  void Remove(Author author);
  Task SaveChangesAsync();
}
