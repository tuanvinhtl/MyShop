namespace TeduShop.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OderID { set; get; }
        public virtual OrderViewModel Order { set; get; }
        public int ProductID { set; get; }
        public virtual ProductViewModel Product { set; get; }
        public int? Quantity { set; get; }
        public decimal Price { set; get; }
    }
}