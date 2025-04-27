using CommonLibrary.MODEL;

namespace Vizsgafeladat.VIEWMODEL
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<int> PermittedPages { get; set; } = new();
    }
}
