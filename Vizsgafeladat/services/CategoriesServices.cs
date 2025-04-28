using CommonLibrary.MODEL;

namespace Vizsgafeladat.services
{
    public class CategoriesServices
    {
        
            private readonly HttpClient _httpClient;

            public CategoriesServices(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<Category>> GetCategory()
            {
                return await _httpClient.GetFromJsonAsync<List<Category>>("api/Category/");
            }

            public async Task<Category> GetProductById(int id)
            {
                return await _httpClient.GetFromJsonAsync<Category>($"api/Category/{id}");
            }

            public async Task CreateProduct(Category category)
            {
                await _httpClient.PostAsJsonAsync("api/Category", category);
            }

            public async Task DeleteProduct(int categoryId)
            {
                await _httpClient.DeleteAsync($"api/Category/{categoryId}");
            }
        }
    }

