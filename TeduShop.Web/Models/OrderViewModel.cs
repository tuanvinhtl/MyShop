using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;

namespace TeduShop.Web.Models
{
    public class OrderViewModel
    {

        public int ID { set; get; }


        public string CustumerName { set; get; }


        public string CustummerAddress { set; get; }


        public string CustummerEmail { set; get; }

        public string CustummerMobile { set; get; }

        public string CustummerMessage { set; get; }

        public DateTime? CreatedDate { set; get; }
        
        public string CreateBy { set; get; }

   
        public string PaymentMethod { set; get; }

  
        public string PaymentStatus { set; get; }

        public bool Status { set; get; }

        public string UserId { set; get; }

        public virtual ApplicationUser ApplicationUser { set; get; }

        public virtual IEnumerable<OrderDetailViewModel> OderDetails { set; get; }
    }
}