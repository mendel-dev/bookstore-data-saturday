using System.Net;
using System.Net.Http.Json;
using BookStore.Shared.Authors;

namespace BookStore.Web.Services;

public class AuthorApiClient
{
  private readonly HttpClient _http;

  public AuthorApiClient(HttpClient http)
  {
    _http = http;
  }

  public async Task<List<AuthorResponse>> GetAllAsync()
  {
    return await _http.GetFromJsonAsync<List<AuthorResponse>>("api/authors") ?? [];
  }

  public async Task<AuthorResponse?> GetByIdAsync(int id)
  {
    var response = await _http.GetAsync($"api/authors/{id}");
    if (response.StatusCode == HttpStatusCode.NotFound)
    {
      return null;
    }

    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<AuthorResponse>();
  }

  public async Task<AuthorResponse> CreateAsync(AuthorRequest request)
  {
    var response = await _http.PostAsJsonAsync("api/authors", request);
    await ApiException.EnsureSuccessAsync(response);
    return (await response.Content.ReadFromJsonAsync<AuthorResponse>())!;
  }

  public async Task<AuthorResponse?> UpdateAsync(int id, AuthorRequest request)
  {
    var response = await _http.PutAsJsonAsync($"api/authors/{id}", request);
    if (response.StatusCode == HttpStatusCode.NotFound)
    {
      return null;
    }

    await ApiException.EnsureSuccessAsync(response);
    return await response.Content.ReadFromJsonAsync<AuthorResponse>();
  }

  public async Task DeleteAsync(int id)
  {
    var response = await _http.DeleteAsync($"api/authors/{id}");
    await ApiException.EnsureSuccessAsync(response);
  }
}
