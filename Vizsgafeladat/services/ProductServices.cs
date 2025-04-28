using CommonLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace Vizsgafeladat.services
{
    public class ProductServices
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/Product/");
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/Product/{id}");
        }

        public async Task CreateProduct(Product product)
        {
            await _httpClient.PostAsJsonAsync("api/Product", product);
        }

        public async Task DeleteProduct(int productId)
        {
            await _httpClient.DeleteAsync($"api/Product/{productId}");
        }
    }
}