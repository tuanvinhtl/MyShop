using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("BangNhanMay")]
  public class NhanMay
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public virtual KhachHang KhachHang { set; get; }
        public virtual MayTinh MayTinh { set; get; }

        public DateTime NgayNhan { set; get; }
        public string TinhTrangMay { set; get; }
    }
}
