using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Models
{
    public class Product
    {
        [Key    ]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string? ProductName { get; set; }
        [StringLength(300)]
        public string?  ProductDecription { get; set; }
        [ForeignKey("Category")]
        public int?  CatogeryId { get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public decimal? ProductPrice { get; set; }
        [Column(TypeName = "decimal(2,2)")]

        public decimal? ProductDiscount { get; set; }
        [StringLength(300)]

        public string?  ProductPhoto { get; set; }
        [ForeignKey("Size")]
        public int  SizeId { get; set; }
        [ForeignKey("Color")]

        public int  ColorId { get; set; }
        public bool IsTrend { get; set; }
        public bool  IsNew { get; set; }
        public Category? Category { get; set; }
        public Size? Size { get; set; }
        public Color? Color { get; set; }

    }
}
