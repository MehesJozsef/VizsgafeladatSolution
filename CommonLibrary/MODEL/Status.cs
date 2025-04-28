using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.MODEL
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}
