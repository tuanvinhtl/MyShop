using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class MayTinhViewModel
    {

        public int ID { set; get; }
        public string Name { set; get; }
        public bool? CategoryPC { set; get; }
        public string Desciption { set; get; }
        public DateTime? CreatedDate { set; get; }
        public bool Status { set; get; }
        public string CreatedBy { set; get; }
        public virtual IEnumerable<ChiTietSuaChuaViewModel> BangChiTietSuaChua { set; get; }
    }
}