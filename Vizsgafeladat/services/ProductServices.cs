using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using Vizsgafeladat.MODEL;

namespace Vizsgafeladat.services
{
    public class ProductServices
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<products>> GetAllProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<products>>("api/Product/");
        }

        public async Task<products> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<products>($"api/Product/{id}");
        }

        public async Task CreateProduct(products product)
        {
            await _httpClient.PostAsJsonAsync("api/Product", product);
        }

        public async Task DeleteProduct(int productId)
        {
            await _httpClient.DeleteAsync($"api/Product/{productId}");
        }
    }
}