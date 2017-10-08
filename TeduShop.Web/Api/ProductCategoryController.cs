using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;
using AutoMapper;
using System.Web.Script.Serialization;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    var model = _productCategoryService.Create(productCategory);
                    var mapper = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                    _productCategoryService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var model = _productCategoryService.GetAll();

                var modelViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _productCategoryService.GetAll(keyWord);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var modelViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = modelViewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getallParent")]
        [HttpGet]
        public HttpResponseMessage GetAllParent(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var model = _productCategoryService.GetAll();
                var modelViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var model = _productCategoryService.GetById(id);
                var modelViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var updateProductCategory = _productCategoryService.GetById(productCategoryViewModel.ID);
                    updateProductCategory.UpdateProductCategory(productCategoryViewModel);
                    updateProductCategory.UpdateDate = DateTime.Now;
                    _productCategoryService.Update(updateProductCategory);
                    _productCategoryService.SaveChange();

                    var mapper = Mapper.Map<ProductCategory, ProductCategoryViewModel>(updateProductCategory);

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }

        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _productCategoryService.Delete(id);
                var mapper = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                _productCategoryService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [Route("deletemutile")]
        public HttpResponseMessage DeleteMutile(HttpRequestMessage request, string listProductCategoryId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var listId =new JavaScriptSerializer().Deserialize<List<int>>(listProductCategoryId);
                foreach (var item in listId)
                {
                    _productCategoryService.Delete(item);
                } 
                _productCategoryService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK,listId.Count());
                return response;
            });
        }
    }
}
