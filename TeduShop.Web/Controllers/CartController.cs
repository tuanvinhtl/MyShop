using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class CartController : Controller
    {
        IProductService _productService;
        private ApplicationUserManager _userManager;
        IOrderService _orderService;
        public CartController(IProductService productService, ApplicationUserManager userManager, IOrderService orderService)
        {
            this._productService = productService;
            this._userManager = userManager;
            this._orderService = orderService;

        }
        // GET: Cart
        public ActionResult Index()
        {
            CheckSession();
            return View();
        }

        [HttpPost]
        public JsonResult AddToCart(int productId)
        {
            CheckSession();
            var cart = (List<CartViewModel>)Session[CommonConstain.CARTSESSION];
            if (cart.Any(x => x.CartId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.CartId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                CartViewModel cartViewModel = new CartViewModel();
                cartViewModel.CartId = productId;
                var product = _productService.GetById(productId);
                cartViewModel.Product = AutoMapper.Mapper.Map<Product, ProductViewModel>(product);
                cartViewModel.Quantity = 1;
                cart.Add(cartViewModel);
            }
            Session[CommonConstain.CARTSESSION] = cart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            var cart = (List<CartViewModel>)Session[CommonConstain.CARTSESSION];
            return Json(new
            {
                data = cart,
                status=true
            },JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<CartViewModel>>(cartData);
            var cartSession= (List<CartViewModel>)Session[CommonConstain.CARTSESSION];

            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.CartId==jitem.CartId)
                    {
                        item.Quantity = jitem.Quantity; 
                    }
                }
            }
            Session[CommonConstain.CARTSESSION] = cartSession;
            return Json(new
            {
                status=true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstain.CARTSESSION] = new List<CartViewModel>();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult GetInfoUserLogin()
        {
            if (Request.IsAuthenticated)
            {
                var userID = User.Identity.GetUserId();
                var user = _userManager.FindById(userID);
                return Json(new
                {
                    status=true,
                    data = user
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
            
        [HttpPost]
        public JsonResult RemoveItem(int productId)
        {
            var cart = (List < CartViewModel > )Session[CommonConstain.CARTSESSION];
            cart.RemoveAll(x => x.CartId == productId);
            Session[CommonConstain.CARTSESSION] = cart;
            return Json(new
            {
                status = true
            });
        }

        private void CheckSession()
        {
            if (Session[CommonConstain.CARTSESSION] == null)
            {
                Session[CommonConstain.CARTSESSION] = new List<CartViewModel>();
            }
        }

        [HttpPost]
        public JsonResult CreateOrder(string orderViewModel)
        {
            try
            {
                var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
                var orderNew = new Order();
                orderNew.UpdateOrder(order);

                var cart = (List<CartViewModel>)Session[CommonConstain.CARTSESSION];
                List<OrderDetail> orderDetail = new List<OrderDetail>();
                foreach (var item in cart)
                {
                    var detail = new OrderDetail();
                    detail.ProductID = item.CartId;
                    detail.Quantity = item.Quantity;
                    detail.Price = item.Product.Price;
                    orderDetail.Add(detail);
                }
                if (Request.IsAuthenticated)
                {
                    orderNew.UserId = User.Identity.GetUserId();
                    orderNew.CreateBy = User.Identity.GetUserName();
                }
                _orderService.CreateOrder(orderNew, orderDetail);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {

                return Json(new
                {
                    status = false
                });
            }
            
        }
    }

}