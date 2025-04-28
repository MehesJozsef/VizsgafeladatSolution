using Microsoft.EntityFrameworkCore;
using CommonLibrary.MODEL;

public class UserService
{
    private readonly HttpClient _http;

    public User? CurrentUser { get; private set; }

    public event Action? OnChange;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> Register(User user)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", user);
        return response.IsSuccessStatusCode;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        User loggingUser = new User();
        loggingUser.Id = 0;
        loggingUser.Name = "name";
        loggingUser.Email = email;
        loggingUser.PasswordHash = password;

        var response = await _http.PostAsJsonAsync($"api/User/login", loggingUser);
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User>();
            OnChange?.Invoke();
            return user;
        }

        return null;
    }

    public async Task<List<int>> GetPermittedPages(int userId)
    {
        var result = await _http.GetFromJsonAsync<List<int>>($"api/User/GetpermittedPages/{userId}");
        return result ?? new List<int>();
    }

    public async Task<User?> RegisterAsync(User newUser)
    {
        var response = await _http.PostAsJsonAsync($"api/User/register", newUser);
        if (response.IsSuccessStatusCode)
        {
            var createdUser = await response.Content.ReadFromJsonAsync<User>();
            CurrentUser = createdUser;
            OnChange?.Invoke();
            return createdUser;
        }

        return null;
    }
    /// <summary>
    /// Hozzáadja a jogosultságot a felhasználóhoz
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="directive"></param>
    /// <returns></returns>
    public async Task<bool> GrantAccessToPage(int userId, string directive)
    {
        var response = await _http.PostAsync($"api/user/grant-access?userId={userId}&directive={Uri.EscapeDataString(directive)}", null);
        return response.IsSuccessStatusCode;
    }

    public void Logout()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
}
