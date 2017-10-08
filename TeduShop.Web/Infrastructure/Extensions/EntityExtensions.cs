using System;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.ID = productViewModel.ID;
            product.Name = productViewModel.Name;
            product.Alias = productViewModel.Alias;
            product.CategoryID = productViewModel.CategoryID;
            product.Images = productViewModel.Images;
            product.MoreImages = productViewModel.MoreImages;
            product.Price = productViewModel.Price;
            product.OriginalPrice = productViewModel.OriginalPrice;
            product.Quantity = productViewModel.Quantity;
            product.Promotion = productViewModel.Promotion;
            product.Waranty = productViewModel.Waranty;
            product.Content = productViewModel.Content;
            product.HomeFlag = productViewModel.HomeFlag;
            product.TopHot = productViewModel.TopHot;
            product.Tags = productViewModel.Tags;
            product.ViewCount = productViewModel.ViewCount;
            product.CreatedDate = productViewModel.CreatedDate;
            product.CreatedBy = productViewModel.CreatedBy;
            product.UpdateDate = productViewModel.UpdateDate;
            product.MetaKeywork = productViewModel.MetaKeywork;
            product.Descryption = productViewModel.Descryption;
            product.DisplayOrder = productViewModel.DisplayOrder;
            product.Status = productViewModel.Status;
        }
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.ParentID = productCategoryViewModel.ParentID;
            productCategory.Images = productCategoryViewModel.Images;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;
            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.UpdateDate = productCategoryViewModel.UpdateDate;
            productCategory.UpdateBy = productCategoryViewModel.UpdateBy;
            productCategory.MetaKeywork = productCategoryViewModel.MetaKeywork;
            productCategory.Descryption = productCategoryViewModel.Descryption;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.Status = productCategoryViewModel.Status;
            productCategory.ForWomen = productCategoryViewModel.ForWomen;
            productCategory.ForKid = productCategoryViewModel.ForKid;
            productCategory.ForMan = productCategoryViewModel.ForWomen;
            productCategory.ProductCategoryParentID = productCategoryViewModel.ProductCategoryParentID;
        }

        public static void UpdateKhachHang(this KhachHang khachhang, KhachHangViewModel khachhangVM)
        {
            khachhang.ID = khachhangVM.ID;
            khachhang.Name = khachhangVM.Name;
            khachhang.PhoneNumber = khachhangVM.PhoneNumber;
            khachhang.Address = khachhangVM.Address;
        }
        public static void UpdateMayTinh(this MayTinh maytinh, MayTinhViewModel maytinhVM)
        {
            maytinh.ID = maytinhVM.ID;
            maytinh.Name = maytinhVM.Name;
            maytinh.CategoryPC = maytinhVM.CategoryPC;
            maytinh.CreatedBy = maytinhVM.CreatedBy;
            maytinh.Desciption = maytinhVM.Desciption;
            maytinh.Status = maytinhVM.Status;
            maytinh.CreatedDate = DateTime.Now;
        }
        public static void UpdateChiTietSuaChua(this ChiTietSuaChua chitietSuaChua, ChiTietSuaChuaViewModel chitietSuaChuaVM)
        {
            chitietSuaChua.IDKhachHang = chitietSuaChuaVM.IDKhachHang;
            chitietSuaChua.IDMayTinh = chitietSuaChuaVM.IDMayTinh;
            chitietSuaChua.MoTaSuaChua = chitietSuaChuaVM.MoTaSuaChua;
            chitietSuaChua.NguoiSuaChua = chitietSuaChuaVM.NguoiSuaChua;
            chitietSuaChua.NgaySuaChua = DateTime.Now;
            chitietSuaChua.TrangThai = chitietSuaChuaVM.TrangThai;

        }
        public static void UpdateParentProductCategory(this ParentProductCategory parentProductCategory, ParentProductCategoryViewModel parentProductCategoryVM)
        {
            parentProductCategory.ID = parentProductCategoryVM.ID;
            parentProductCategory.Name = parentProductCategoryVM.Name;

        }
        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVM)
        {
            feedback.ID = feedbackVM.ID;
            feedback.Name = feedbackVM.Name;
            feedback.Message = feedbackVM.Message;
            feedback.Subject = feedbackVM.Subject;
            feedback.Status = feedbackVM.Status;
            feedback.Email = feedbackVM.Email;
            feedback.CreatedDate = DateTime.Now;
        }
        public static void UpdateOrder(this Order order, OrderViewModel orderViewModel)
        {
            order.ID = orderViewModel.ID;
            order.CustumerName = orderViewModel.CustumerName;
            order.CustummerEmail = orderViewModel.CustummerEmail;
            order.CustummerAddress = orderViewModel.CustummerAddress;
            order.CustummerMessage = orderViewModel.CustummerMessage;
            order.Status = false;
            order.UserId = orderViewModel.UserId;
            order.CustummerMobile = orderViewModel.CustummerMobile;
            order.PaymentStatus = orderViewModel.PaymentStatus;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.CreateBy = orderViewModel.CreateBy;
            order.CreatedDate = DateTime.Now;
        }
        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupVM)
        {
            appGroup.ID = appGroupVM.ID;
            appGroup.Name = appGroupVM.Name;
            appGroup.Description = appGroupVM.Description;
        }
        public static void UpdateApplicationUser(this ApplicationUser appUser, ApplicationUserViewModel appUserVM)
        {
            appUser.Id = appUserVM.Id;
            appUser.FullName = appUserVM.FullName;
            appUser.BirthDay = appUserVM.BirthDay;
            appUser.Email = appUserVM.Email;
            appUser.UserName = appUserVM.UserName;
            appUser.PhoneNumber = appUserVM.PhoneNumber;
            appUser.CreatedDate = DateTime.Now;
        }
        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleVM, string Action)
        {
            if (Action=="create")
            {
                appRole.Id = Guid.NewGuid().ToString();
                appRole.Name = appRoleVM.Name;
                appRole.Description = appRoleVM.Description;
            }
            if (Action=="update")
            {
                appRole.Id = appRoleVM.Id;
                appRole.Name = appRoleVM.Name;
                appRole.Description = appRoleVM.Description;
            }
        }
    }

}