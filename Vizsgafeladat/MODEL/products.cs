using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vizsgafeladat.MODEL
{
    public class products
    {
        [Key]
        public int product_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public string image_url { get; set; }
        public int category_id { get; set; }
    }
}
