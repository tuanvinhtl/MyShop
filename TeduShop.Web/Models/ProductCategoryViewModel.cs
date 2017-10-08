using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ProductCategoryViewModel
    {

        public int ID { set; get; }
        
        public string Name { set; get; }


        public string Alias { set; get; }

        public int? ParentID { set; get; }

        public string Images { set; get; }

        public bool? HomeFlag { set; get; }
        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdateDate { set; get; }

        public string UpdateBy { set; get; }

        public string MetaKeywork { set; get; }

        public string Descryption { set; get; }

        public int ProductCategoryParentID { set; get; }
        public virtual ParentProductCategoryViewModel ParentProductCategory { set; get; }
        public int? DisplayOrder { set; get; }
        public bool? ForWomen { set; get; }
        public bool? ForKid { set; get; }
        public bool? ForMan { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }
    }
}