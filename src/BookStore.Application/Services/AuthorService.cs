using BookStore.Application.Repositories;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authors;

    public AuthorService(IAuthorRepository authors)
    {
        _authors = authors;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await _authors.GetAllWithBooksAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await _authors.GetByIdWithBooksAsync(id);
    }

    public async Task<Author> CreateAsync(Author author)
    {
        if (string.IsNullOrWhiteSpace(author.Name))
        {
            throw new InvalidOperationException("Author name is required.");
        }

        if (author.Name.Length > 150)
        {
            throw new InvalidOperationException("Author name must be at most 150 characters.");
        }

        if (author.BirthDate > DateTime.UtcNow)
        {
            throw new InvalidOperationException("Birth date cannot be in the future.");
        }

        author.Name = author.Name.Trim();
        author.Nationality = (author.Nationality ?? string.Empty).Trim();
        author.Bio = (author.Bio ?? string.Empty).Trim();

        await _authors.AddAsync(author);
        await _authors.SaveChangesAsync();
        return author;
    }

    public async Task<Author?> UpdateAsync(int id, Author author)
    {
        var existing = await _authors.GetByIdAsync(id);
        if (existing is null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(author.Name))
        {
            throw new InvalidOperationException("Author name is required.");
        }

        if (author.BirthDate > DateTime.UtcNow)
        {
            throw new InvalidOperationException("Birth date cannot be in the future.");
        }

        existing.Name = author.Name.Trim();
        existing.Bio = (author.Bio ?? string.Empty).Trim();
        existing.Nationality = (author.Nationality ?? string.Empty).Trim();
        existing.BirthDate = author.BirthDate;

        await _authors.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _authors.GetByIdWithBooksAsync(id);

        if (existing is null)
        {
            return false;
        }

        if (existing.Books.Any())
        {
            throw new InvalidOperationException("Cannot delete an author who still has books.");
        }

        _authors.Remove(existing);
        await _authors.SaveChangesAsync();
        return true;
    }
}
