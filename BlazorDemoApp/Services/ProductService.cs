using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorDemoApp;

public class ProductService : IProductService
{
    private readonly HttpClient client;

    private readonly JsonSerializerOptions options;

    public ProductService(HttpClient httpClient)
    {
        client = httpClient;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Product>?> GetProducts()
    {
        var response = await client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Product>>(content, options);
    }

    public async Task AddProduct(Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task DeleteProduct(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}

public interface IProductService
{
    Task<List<Product>?> GetProducts();

    Task AddProduct(Product product);

    Task DeleteProduct(int productId);
}