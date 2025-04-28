using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.MODEL
{
    public class OrderHead
    {
        [Key]
        public int OrderHeadId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public int StatusId { get; set; }
        public string? Remark { get; set; }
    }
}
