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
    [RoutePrefix("api/chitietsuachua")]
    public class ChiTietSuaChuaController : ApiControllerBase
    {
        IChiTietSuaChuaService _chitietSuaChuaService;
        IMayTinhService _maytinhService;
        IKhachHangService _khachhangService;
        public ChiTietSuaChuaController(IKhachHangService khachhangService,IMayTinhService maytinhService, IChiTietSuaChuaService chitietSuaChuaService, IErrorService errorService) : base(errorService)
        {
            this._chitietSuaChuaService = chitietSuaChuaService;
            this._maytinhService = maytinhService;
            this._khachhangService = khachhangService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _chitietSuaChuaService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.IDMayTinh).Skip(page * pageSize).Take(pageSize);
                var modelViewModel = Mapper.Map<IEnumerable<ChiTietSuaChua>, IEnumerable<ChiTietSuaChuaViewModel>>(query);
                var paginationSet = new PaginationSet<ChiTietSuaChuaViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, ChiTietSuaChuaViewModel chitietSuaChuaViewModel)
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
                    ChiTietSuaChua khachhang = new ChiTietSuaChua();
                    khachhang.UpdateChiTietSuaChua(chitietSuaChuaViewModel);
                    var created = _chitietSuaChuaService.Create(khachhang);
                    var mapper = Mapper.Map<ChiTietSuaChua, ChiTietSuaChuaViewModel>(created);
                    _chitietSuaChuaService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }
        [Route("createMutile")]
        [HttpPost]
        public HttpResponseMessage CreateMutile(HttpRequestMessage request, ChiTietSuaChuaViewModel chitietSuaChuaViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

    
                    MayTinh maytinh = new MayTinh()
                    {
                        Name = chitietSuaChuaViewModel.MayTinh.Name,
                        CategoryPC=chitietSuaChuaViewModel.MayTinh.CategoryPC,
                        Desciption=chitietSuaChuaViewModel.MayTinh.Desciption,
                        Status=chitietSuaChuaViewModel.MayTinh.Status,
                    };
                    var createdMayTinh= _maytinhService.Create(maytinh);
                    _maytinhService.SaveChange();

                    KhachHang khachhang = new KhachHang()
                    {
                        Name=chitietSuaChuaViewModel.KhachHang.Name,
                        Address=chitietSuaChuaViewModel.KhachHang.Address,
                        PhoneNumber=chitietSuaChuaViewModel.KhachHang.PhoneNumber
                    };
                    var createdKhachHang= _khachhangService.Create(khachhang);
                    _khachhangService.SaveChange();

                    ChiTietSuaChua chitietsuachua = new ChiTietSuaChua();
                    chitietSuaChuaViewModel.IDMayTinh = createdMayTinh.ID;
                    chitietSuaChuaViewModel.IDKhachHang = createdKhachHang.ID;                   
                    chitietsuachua.UpdateChiTietSuaChua(chitietSuaChuaViewModel);
                    
                    var created = _chitietSuaChuaService.Create(chitietsuachua);
                    var mapper = Mapper.Map<ChiTietSuaChua, ChiTietSuaChuaViewModel>(created);
                    _chitietSuaChuaService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
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
                var delete = _chitietSuaChuaService.Delete(id);
                var mapper = Mapper.Map<ChiTietSuaChua, ChiTietSuaChuaViewModel>(delete);
                _chitietSuaChuaService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ChiTietSuaChuaViewModel chitietSuaChuaViewModel)
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
                    ChiTietSuaChua khachhang = new ChiTietSuaChua();
                    khachhang.UpdateChiTietSuaChua(chitietSuaChuaViewModel);
                    _chitietSuaChuaService.Update(khachhang);
                    _chitietSuaChuaService.SaveChange();
                    var mapper = Mapper.Map<ChiTietSuaChua, ChiTietSuaChuaViewModel>(khachhang);
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

                var model = _chitietSuaChuaService.GetById(id);
                var modelViewModel = Mapper.Map<ChiTietSuaChua, ChiTietSuaChuaViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }
    }
}
