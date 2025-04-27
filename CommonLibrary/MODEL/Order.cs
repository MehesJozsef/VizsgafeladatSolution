using System.Collections.Generic;

namespace CommonLibrary.MODEL
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerEmail { get; set; }
        //public List<Product> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
