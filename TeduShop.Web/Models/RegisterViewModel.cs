using System.ComponentModel.DataAnnotations;

namespace TeduShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Bạn phải nhập Tên ")]
        public string FullName { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu ")]
        public string Password { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập Email ")]
        public string Email { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }


    }
}