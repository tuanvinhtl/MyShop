using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class FeedbackViewModel
    {

        public int ID { set; get; }

        [Required(ErrorMessage ="Phải nhập tên của bạn")]
        [StringLength(250)]       
        public string Name { set; get; }

        public string Subject { set; get; }

        [StringLength(250)]
        [Required(ErrorMessage = "Phải nhập Email của bạn")]
        public string Email { set; get; }

        [StringLength(500)]
        [Required(ErrorMessage = "Phải nhập nội dung thư")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}