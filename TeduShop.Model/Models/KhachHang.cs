using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("BangKhachHang")]
    public  class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [Required]
        public string Name { set; get; }
        public string Address { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        public virtual IEnumerable<ChiTietSuaChua> BangChiTietSuaChua { set; get; }

    }
}
