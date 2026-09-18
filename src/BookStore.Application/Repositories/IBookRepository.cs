using BookStore.Domain.Entities;

namespace BookStore.Application.Repositories;

public interface IBookRepository
{
  Task<List<Book>> GetAllWithAuthorAsync();
  Task<Book?> GetByIdWithAuthorAsync(int id);
  Task<Book?> GetByIdAsync(int id);
  Task AddAsync(Book book);
  void Remove(Book book);
  Task SaveChangesAsync();
}
