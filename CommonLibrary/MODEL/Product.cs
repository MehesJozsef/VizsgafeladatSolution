using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLibrary.MODEL
{
    public class Product
    {
        [Key]
        public int Product_id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }

        public string? Image_url { get; set; }
        public int Category_id { get; set; }
    }
}
