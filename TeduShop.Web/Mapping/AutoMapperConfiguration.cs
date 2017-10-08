using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configuration()
        {
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<OrderDetail, OrderDetailViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<ProductTag, ProductTagViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<KhachHang, KhachHangViewModel>();
            Mapper.CreateMap<MayTinh, MayTinhViewModel>();
            Mapper.CreateMap<ChiTietSuaChua, ChiTietSuaChuaViewModel>();
            Mapper.CreateMap<ParentProductCategory, ParentProductCategoryViewModel>();
            Mapper.CreateMap<Slide, SlideViewModel>();
            Mapper.CreateMap<Contact, ContactViewModel>();
            Mapper.CreateMap<Feedback, FeedbackViewModel>();
            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
        }
    }
}