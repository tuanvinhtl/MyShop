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
    [RoutePrefix("api/maytinh")]
    [Authorize]
    public class MayTinhController : ApiControllerBase
    {
        IMayTinhService _maytinhService;
        public MayTinhController(IMayTinhService maytinhService,IErrorService errorService) : base(errorService)
        {
            this._maytinhService = maytinhService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request,string keyWord,int page,int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _maytinhService.GetAll(keyWord);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var mapperViewModel = Mapper.Map<IEnumerable<MayTinh>, IEnumerable<MayTinhViewModel>>(query);
                var paginationSet = new PaginationSet<MayTinhViewModel>()
                {
                    Items = mapperViewModel,
                    Page=page,
                    TotalCount=totalRow,
                    TotalPages= (int)Math.Ceiling((decimal) totalRow/pageSize),
                };
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, MayTinhViewModel maytinhViewModel)
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
                    MayTinh maytinh = new MayTinh();
                    maytinh.UpdateMayTinh(maytinhViewModel);
                    var created = _maytinhService.Create(maytinh);
                    var mapper = Mapper.Map<MayTinh, MayTinh>(created);
                    _maytinhService.SaveChange();

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
                var delete = _maytinhService.Delete(id);
                var mapper = Mapper.Map<MayTinh, MayTinhViewModel>(delete);
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                _maytinhService.SaveChange();
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, MayTinhViewModel maytinhViewModel)
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
                    MayTinh maytinh = new MayTinh();
                    maytinh.UpdateMayTinh(maytinhViewModel);
                    _maytinhService.Update(maytinh);
                    _maytinhService.SaveChange();
                    var mapper = Mapper.Map<MayTinh, MayTinhViewModel>(maytinh);
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

                var model = _maytinhService.GetById(id);
                var modelViewModel = Mapper.Map<MayTinh, MayTinhViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }
    }
}
