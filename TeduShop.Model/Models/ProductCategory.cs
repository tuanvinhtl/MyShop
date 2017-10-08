using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("ProductCategories")]
    public class ProductCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        [Required]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        public int? ParentID { set; get; }

        public int ProductCategoryParentID { set; get; }

        [ForeignKey("ProductCategoryParentID")]
        public virtual ParentProductCategory ParentProductCategory { set; get; }
        public string Images { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? ForWomen { set; get; }
        public bool? ForKid { set; get; }
        public bool? ForMan { set; get; }
        public virtual IEnumerable<Product> Products { set; get; }
    }
}
