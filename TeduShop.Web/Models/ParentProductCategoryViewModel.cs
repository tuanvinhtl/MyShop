using System.Collections.Generic;

namespace TeduShop.Web.Models
{
    public class ParentProductCategoryViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public IEnumerable<ProductCategoryViewModel> ProductCategories { set; get; }
    }
}