using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.MODEL
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderHeadId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int LineTotal { get; set; }
    }
}
