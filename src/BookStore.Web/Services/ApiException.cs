using System.Net.Http.Json;

namespace BookStore.Web.Services;

public class ApiException : Exception
{
  public ApiException(string message) : base(message)
  {
  }

  public static async Task EnsureSuccessAsync(HttpResponseMessage response)
  {
    if (response.IsSuccessStatusCode)
    {
      return;
    }

    var problem = await response.Content.ReadFromJsonAsync<ApiProblemDetails>();
    throw new ApiException(problem?.Detail ?? "The request could not be completed.");
  }
}

internal class ApiProblemDetails
{
  public string? Detail { get; set; }
}
