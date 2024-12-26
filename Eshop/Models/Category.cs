using System.ComponentModel.DataAnnotations;

namespace Eshop.Models
{
    public class Category
    {
        [Key]
        public int CategoryId    { get; set; }
        [StringLength(100)]                         
        public string? CategoryName { get; set; }
        [StringLength(100)]
        public string? CategoryPhoto { get; set; }

        public int CategoryOrderBy { get; set; }
    }
}
