using CommonLibrary.MODEL;

public class AuthStateService
{
    public User? CurrentUser { get; private set; }

    public event Action? OnChange;

    public void SetUser(User? user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }

    public bool IsLoggedIn => CurrentUser != null;
}
