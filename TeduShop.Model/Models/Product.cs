using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(256)]
        [Required]
        public string Name { set; get; }

        [StringLength(256)]
        [Required]
        public string Alias { set; get; }

        [Required]
        public int CategoryID { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }

        public string Images { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal? OriginalPrice { set; get; }
        public int Quantity { set; get; }

        public decimal? Promotion { set; get; }

        public int? Waranty { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? TopHot { set; get; }
        public string Tags { set; get; }
        public int? ViewCount { set; get; }
        public virtual IEnumerable<OrderDetail> OderDetails { set; get; }
        public virtual IEnumerable<ProductTag> ProductTags { set; get; }

    }
}
