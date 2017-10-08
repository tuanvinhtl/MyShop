using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class SlideViewModel
    {

        public int ID { set; get; }


        public string Name { set; get; }


        public string Descryption { set; get; }

        public string Content { set; get; }


        public int GiamGia { set; get; }

        public string Images { set; get; }

        public string URL { set; get; }

        public int DisplayOrder { set; get; }

        public bool Status { set; get; }
    }
}