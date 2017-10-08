using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class PostViewModel
    {

        public int ID { set; get; }


        public string Name { set; get; }


        public string Alias { set; get; }


        public int CaregoryID { set; get; }


        public virtual PostCategoryViewModel PostCategory { set; get; }

        public string Images { set; get; }


        public string MoreImages { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }
        public DateTime? CreatedDate { set; get; }


        public string CreatedBy { set; get; }

        public DateTime? UpdateDate { set; get; }


        public string UpdateBy { set; get; }

        public string MetaKeywork { set; get; }

        public string Descryption { set; get; }

        public int? DisplayOrder { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<PostTagViewModel> PostTags { set; get; }
    }
}