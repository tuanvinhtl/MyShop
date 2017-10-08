using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ChiTietSuaChuaViewModel
    {

        public int IDKhachHang { set; get; }

        public int IDMayTinh { set; get; }
        public string MoTaSuaChua { set; get; }
        public DateTime NgaySuaChua { set; get; }
        public string NguoiSuaChua { set; get; }
        public int? BaoHanh { set; get; }
        public bool TrangThai { set; get; }
        public virtual KhachHangViewModel KhachHang { set; get; }

        public virtual MayTinhViewModel MayTinh { set; get; }
    }
}