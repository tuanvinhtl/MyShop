using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IParentProductCategoryService _parentProductCategoryService;
        IProductService _productService;
        ISlideService _slideService;
        
        public HomeController(ISlideService slideService, IProductService productService ,IParentProductCategoryService parentProductCategoryService, IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
            _parentProductCategoryService = parentProductCategoryService;
            _productService = productService;
            _slideService = slideService;
        }
        public ActionResult Index()
        {
            var getSlide = _slideService.GetAll();

            var getTrendingTop = _productService.GetProductTrendingTop(4);
            var getTrendingBot = _productService.GetProductTrendingBot(4);

            var mapperSlide = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(getSlide);
            var mapperTrendingTop = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(getTrendingTop);
            var mapperTrendingBot = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(getTrendingBot);

            var homeViewMode = new HomeViewModel();
            homeViewMode.Slides = mapperSlide;
            homeViewMode.TrendingItemsTop = mapperTrendingTop;
            homeViewMode.TrendingItemsBot = mapperTrendingBot;

            return View(homeViewMode);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Brand()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {

            var getProductCategory = _productCategoryService.GetAll();
            List<ProductCategory> p = new List<ProductCategory>(getProductCategory);
            var getall = _parentProductCategoryService.GetAll();
            List<ParentProductCategory> ps = new List<ParentProductCategory>(getall);

            foreach (var item in p)
            {
                var getID = _parentProductCategoryService.GetParentProductCategory(item.ProductCategoryParentID);
                foreach (var i in ps)
                {

                    if (item.ProductCategoryParentID == i.ID)
                    {
                        i.ProductCategories = getID;
                    }

                }
            }
            ViewBag.ProductCategoryForWoman = Mapper.Map<IEnumerable<ParentProductCategory>, IEnumerable<ParentProductCategoryViewModel>>(ps);
            //ViewBag.ProductCategoryForMan = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategoryForWoman);


            return PartialView();
        }
    }
}