using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("SupportOnlines")]
    public class SupportOnline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        [Required]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Department { set; get; }

        [MaxLength(256)]
        public string Skype { set; get; }

        [MaxLength(256)]
        public string Mobile { set; get; }

        [MaxLength(256)]
        public string Yahoo { set; get; }

        [MaxLength(256)]
        public string FaceBook { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}
