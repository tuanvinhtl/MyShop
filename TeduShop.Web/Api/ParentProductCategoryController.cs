using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/ParentproductCategory")]
    public class ParentProductCategoryController : ApiControllerBase
    {
        IParentProductCategoryService _parentProductCategoryService;

        public ParentProductCategoryController(IParentProductCategoryService parentProductCategoryService,IErrorService errorService):base(errorService)
        {
            this._parentProductCategoryService = parentProductCategoryService;
        }
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                var model = _parentProductCategoryService.GetAll();
                var modelViewModel = Mapper.Map<IEnumerable<ParentProductCategory>, IEnumerable<ParentProductCategoryViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ParentProductCategoryViewModel parentProductCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ParentProductCategory parentProductCategory = new ParentProductCategory();
                    parentProductCategory.UpdateParentProductCategory(parentProductCategoryViewModel);
                    var created = _parentProductCategoryService.Create(parentProductCategory);
                    var mapper = Mapper.Map<ParentProductCategory, ParentProductCategoryViewModel>(created);
                    _parentProductCategoryService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var delete = _parentProductCategoryService.Delete(id);
                var mapper = Mapper.Map<ParentProductCategory, ParentProductCategoryViewModel>(delete);
                _parentProductCategoryService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ParentProductCategoryViewModel parentProductCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ParentProductCategory parentProductCategory = new ParentProductCategory();
                    parentProductCategory.UpdateParentProductCategory(parentProductCategoryViewModel);
                    _parentProductCategoryService.Update(parentProductCategory);
                    _parentProductCategoryService.SaveChange();
                    var mapper = Mapper.Map<ParentProductCategory, ParentProductCategoryViewModel>(parentProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
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

                var model = _parentProductCategoryService.GetById(id);
                var modelViewModel = Mapper.Map<ParentProductCategory, ParentProductCategoryViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }
    }
}
