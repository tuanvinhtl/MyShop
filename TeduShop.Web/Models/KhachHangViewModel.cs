using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class KhachHangViewModel
    {

        public int ID { set; get; }

        public string Name { set; get; }
        public string Address { set; get; }

        public string PhoneNumber { set; get; }
        public virtual IEnumerable<ChiTietSuaChuaViewModel> BangChiTietSuaChua { set; get; }
    }
}