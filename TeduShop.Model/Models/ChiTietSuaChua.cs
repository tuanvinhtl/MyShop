using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("BangChiTietSuaChua")]
    public class ChiTietSuaChua
    {
        [Key]
        [Column(Order = 1)]
        public int IDKhachHang { set; get; }

        [Key]
        [Column(Order = 2)]
        public int IDMayTinh { set; get; }
        public string MoTaSuaChua { set; get; }
        public DateTime NgaySuaChua { set; get; }
        public string NguoiSuaChua { set; get; }
        public int? BaoHanh { set; get; }
        public bool TrangThai { set; get; }

        [ForeignKey("IDKhachHang")]
        public virtual KhachHang KhachHang { set; get; }

        [ForeignKey("IDMayTinh")]
        public virtual MayTinh MayTinh { set; get; }

    }
}
