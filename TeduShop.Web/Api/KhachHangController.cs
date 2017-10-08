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

    [RoutePrefix("api/khachhang")]
    [Authorize]
    public class KhachHangController : ApiControllerBase
    {
        IKhachHangService _khachHangService;
        public KhachHangController(IKhachHangService khachHangService, IErrorService errorService) : base(errorService)
        {
            this._khachHangService = khachHangService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _khachHangService.GetAll(keyWord);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);
                var modelViewModel = Mapper.Map<IEnumerable<KhachHang>, IEnumerable<KhachHangViewModel>>(query);
                var paginationSet = new PaginationSet<KhachHangViewModel>()
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
        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, KhachHangViewModel khachhangViewModel)
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
                    KhachHang khachhang = new KhachHang();
                    khachhang.UpdateKhachHang(khachhangViewModel);
                    var created = _khachHangService.Create(khachhang);
                    var mapper = Mapper.Map<KhachHang, KhachHangViewModel>(created);
                    _khachHangService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request,int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var delete = _khachHangService.Delete(id);
                var mapper = Mapper.Map<KhachHang, KhachHangViewModel>(delete);
                _khachHangService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, KhachHangViewModel khachhangViewModel)
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
                    KhachHang khachhang = new KhachHang();
                    khachhang.UpdateKhachHang(khachhangViewModel);
                    _khachHangService.Update(khachhang);
                    _khachHangService.SaveChange();
                    var mapper = Mapper.Map<KhachHang, KhachHangViewModel>(khachhang);
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

                var model = _khachHangService.GetById(id);
                var modelViewModel = Mapper.Map<KhachHang, KhachHangViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }
    }
}
