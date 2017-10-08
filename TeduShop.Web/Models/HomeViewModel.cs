using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<ProductViewModel> TrendingItemsTop { set; get; }
        public IEnumerable<ProductViewModel> TrendingItemsBot { set; get; }

    }
}