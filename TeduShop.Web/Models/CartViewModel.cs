using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    [Serializable]
    public class CartViewModel
    {
        public int CartId { set; get; }
        public int Quantity { set; get; }
        public ProductViewModel Product { set; get; }
    }
}