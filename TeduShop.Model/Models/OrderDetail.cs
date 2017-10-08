using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public int OderID { set; get; }

        [ForeignKey("OderID")]
        public virtual Order Order { set; get; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public int ProductID { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        public int? Quantity { set; get; }
        public decimal Price { set; get; }
    }
}
