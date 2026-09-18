using System.Net;
using System.Net.Http.Json;
using BookStore.Shared.Books;

namespace BookStore.Web.Services;

public class BookApiClient
{
  private readonly HttpClient _http;

  public BookApiClient(HttpClient http)
  {
    _http = http;
  }

  public async Task<List<BookResponse>> GetAllAsync()
  {
    return await _http.GetFromJsonAsync<List<BookResponse>>("api/books") ?? [];
  }

  public async Task<BookResponse?> GetByIdAsync(int id)
  {
    var response = await _http.GetAsync($"api/books/{id}");
    if (response.StatusCode == HttpStatusCode.NotFound)
    {
      return null;
    }

    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<BookResponse>();
  }

  public async Task<BookResponse> CreateAsync(BookRequest request)
  {
    var response = await _http.PostAsJsonAsync("api/books", request);
    await ApiException.EnsureSuccessAsync(response);
    return (await response.Content.ReadFromJsonAsync<BookResponse>())!;
  }

  public async Task<BookResponse?> UpdateAsync(int id, BookRequest request)
  {
    var response = await _http.PutAsJsonAsync($"api/books/{id}", request);
    if (response.StatusCode == HttpStatusCode.NotFound)
    {
      return null;
    }

    await ApiException.EnsureSuccessAsync(response);
    return await response.Content.ReadFromJsonAsync<BookResponse>();
  }

  public async Task DeleteAsync(int id)
  {
    var response = await _http.DeleteAsync($"api/books/{id}");
    await ApiException.EnsureSuccessAsync(response);
  }
}
