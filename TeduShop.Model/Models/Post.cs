using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("Posts")]
    public class Post : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Alias { set; get; }

        [Required]
        public int CaregoryID { set; get; }

        [ForeignKey("CaregoryID")]
        public virtual PostCategory PostCategory { set; get; }

        public string Images { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public virtual IEnumerable<PostTag> PostTags { set; get; }
    }
}
