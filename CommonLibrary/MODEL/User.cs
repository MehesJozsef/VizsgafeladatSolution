using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.MODEL
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public String? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
    }
}
