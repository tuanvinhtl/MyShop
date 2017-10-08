using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Product
        public ActionResult Index(int id, int page = 1)
        {

            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var model = _productService.GetListProductByPaginatonSet(id, page, pageSize, out totalRow);
            var mapperModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = mapperModel,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }
        public ActionResult Detail(int id)
        {
            var model = _productService.GetById(id);
            var mapper = Mapper.Map<Product, ProductViewModel>(model);
            var listMoreImages = new JavaScriptSerializer().Deserialize<List<string>>(mapper.MoreImages);
            ViewBag.MoreImagesProduct = listMoreImages;
            var tags = _productService.GetListTagByProductId(id);
            var modelTag = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tags);
            ViewBag.Tags = modelTag;
            return View(mapper);
        }

        [ChildActionOnly]
        public ActionResult ProductLeft()
        {
            return PartialView();
        }
    }
}