using System.Text.Json;

namespace BlazorDemoApp;

public class CategoryService : ICategorySercice
{
    private readonly HttpClient client;

    private readonly JsonSerializerOptions options;

    public CategoryService(HttpClient client)
    {
        this.client = client;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Category>?> GetCategory()
    {
        var response = await client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Category>>(content, options);
    }
}

public interface ICategorySercice
{
    Task<List<Category>?> GetCategory();
}