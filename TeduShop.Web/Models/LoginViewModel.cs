using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Không được bỏ trống Tên Đăng Nhập")]
        [MaxLength(256)]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Không được bỏ trống Mật Khẩu")]
        [MaxLength(100)]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}