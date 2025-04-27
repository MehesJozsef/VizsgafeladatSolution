using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.MODEL
{
    public class Category
    {
        [Key]
        public int Category_id {  get; set; }
        public string? Name {  get; set; }
        public string? Description {  get; set; }
    }
}
