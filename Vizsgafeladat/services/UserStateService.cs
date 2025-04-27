using Vizsgafeladat.VIEWMODEL;

namespace Vizsgafeladat.services
{
    public class UserStateService
    {
        /// <summary>
        /// Azonnal észreveszi a ki és belépést
        /// </summary>
        public CurrentUser? CurrentUser { get; private set; }

        public event Action? OnChange;

        public void SetUser(CurrentUser user)
        {
            CurrentUser = user;
            OnChange?.Invoke();
        }

        public void ClearUser()
        {
            CurrentUser = null;
            OnChange?.Invoke();
        }
    }
}
