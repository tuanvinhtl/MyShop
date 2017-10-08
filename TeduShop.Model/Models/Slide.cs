using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("Slides")]
    public class Slide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(256)]
        [Required]
        public string Name { set; get; }

        public string Descryption { set; get; }

        public string Content { set; get; }

        [Required]
        public int GiamGia { set; get; }

        [Required]
        public string Images { set; get; }

        [Required]
        public string URL { set; get; }

        public int DisplayOrder { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}
