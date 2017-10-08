using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(256)]
        public string CustumerName { set; get; }

        [StringLength(256)]
        public string CustummerAddress { set; get; }

        [StringLength(256)]
        public string CustummerEmail { set; get; }

        [StringLength(256)]
        public string CustummerMobile { set; get; }

        [StringLength(256)]
        public string CustummerMessage { set; get; }

        public DateTime? CreatedDate { set; get; }

        [StringLength(256)]
        public string CreateBy { set; get; }

        [StringLength(256)]
        public string PaymentMethod { set; get; }

        [StringLength(256)]
        public string PaymentStatus { set; get; }

        [Required]
        public bool Status { set; get; }


        [MaxLength(128)]
        [Column(TypeName ="nvarchar")]
        public  string UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { set; get; }

        public virtual IEnumerable<OrderDetail> OderDetails { set; get; }
    }
}
