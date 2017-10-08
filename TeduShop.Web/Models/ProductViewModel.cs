using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    [Serializable]
    public class ProductViewModel
    {

        public int ID { set; get; }
        public string Name { set; get; }

        public string Alias { set; get; }

        public int CategoryID { set; get; }

        public virtual ProductCategoryViewModel ProductCategory { set; get; }

        public string Images { set; get; }

        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal? OriginalPrice { set; get; }
        public int Quantity { set; get; }

        public decimal? Promotion { set; get; }

        public int? Waranty { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? TopHot { set; get; }
        public string Tags { set; get; }
        public int? ViewCount { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }

        public DateTime? UpdateDate { set; get; }
        public string UpdateBy { set; get; }

        public string MetaKeywork { set; get; }

        public string Descryption { set; get; }

        public int? DisplayOrder { set; get; }

        public bool Status { set; get; }
        public virtual IEnumerable<OrderDetailViewModel> OderDetails { set; get; }
        public virtual IEnumerable<ProductTagViewModel> ProductTags { set; get; }
    }
}