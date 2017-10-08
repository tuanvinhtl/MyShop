using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { set; get; }

        [StringLength(256)]
        public string Name { set; get; }

        [StringLength(256)]
        public string Address { set; get; }
        public string PhoneNumber { set; get; }

        [StringLength(256)]
        public string Email { set; get; }

        public double Lat { set; get; }
        public double Lng { set; get; }

        [StringLength(500)]
        public string Content { set; get; }
        public bool Status { set; get; }

    }
}
