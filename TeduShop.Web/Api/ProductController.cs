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
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;
using System.Web.Script.Serialization;
using System.Web;
using TeduShop.Common;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using System.Configuration;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }
        [Route("create")]
        [Authorize(Roles = ("Create"))]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel ProductViewModel)
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
                    Product product = new Product();
                    product.UpdateProduct(ProductViewModel);
                    var model = _productService.Create(product);
                    var mapper = Mapper.Map<Product, ProductViewModel>(model);
                    _productService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }
        [Authorize(Roles = ("User"))]
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _productService.GetAll(keyWord);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var modelViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);
                var paginationSet = new PaginationSet<ProductViewModel>()
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


        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = ("User"))]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var model = _productService.GetById(id);
                var modelViewModel = Mapper.Map<Product, ProductViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }

        [Route("update")]
        [Authorize(Roles = ("Update"))]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productViewModel)
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
                    var updateProduct = _productService.GetById(productViewModel.ID);
                    updateProduct.UpdateProduct(productViewModel);
                    updateProduct.UpdateDate = DateTime.Now;
                    _productService.Update(updateProduct);
                    _productService.SaveChange();

                    var mapper = Mapper.Map<Product, ProductViewModel>(updateProduct);

                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }

        [Route("delete")]
        [Authorize(Roles = ("Delete"))]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _productService.Delete(id);
                var mapper = Mapper.Map<Product, ProductViewModel>(model);
                _productService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [Route("deletemutile")]
        [Authorize(Roles = ("Delete"))]
        public HttpResponseMessage DeleteMutile(HttpRequestMessage request, string listProductId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var listId = new JavaScriptSerializer().Deserialize<List<int>>(listProductId);
                foreach (var item in listId)
                {
                    _productService.Delete(item);
                }
                _productService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, listId.Count());
                return response;
            });
        }

        [Route("getall1")]
        [Authorize(Roles = ("User"))]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;


                var model = _productService.GetAll();


                var modelViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelViewModel);
                return response;
            });
        }

        [Route("import")]
        [HttpPost]
        public async Task<HttpResponseMessage> Import()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            //do stuff with files if you wish
            if (result.FormData["categoryId"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn danh mục sản phẩm.");
            }

            //Upload files
            int addedCount = 0;
            int categoryId = 0;
            int.TryParse(result.FormData["categoryId"], out categoryId);
            foreach (MultipartFileData fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fullPath = Path.Combine(root, fileName);
                File.Copy(fileData.LocalFileName, fullPath, true);

                //insert to DB
                var listProduct = this.ReadProductFromExcel(fullPath, categoryId);
                if (listProduct.Count > 0)
                {
                    foreach (var product in listProduct)
                    {
                        _productService.Create(product);
                        addedCount++;
                    }
                    _productService.SaveChange();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " sản phẩm thành công.");
        }
        private List<Product> ReadProductFromExcel(string fullPath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Product> listProduct = new List<Product>();
                ProductViewModel productViewModel;
                Product product;

                decimal originalPrice = 0;
                decimal price = 0;
                decimal promotionPrice;


                bool status = false;
                bool showHome = false;
                bool isHot = false;
                int warranty;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    productViewModel = new ProductViewModel();
                    product = new Product();

                    productViewModel.Name = workSheet.Cells[i, 1].Value.ToString();
                    productViewModel.Alias = StringHelper.ToUnsignString(productViewModel.Name);
                    productViewModel.Descryption = workSheet.Cells[i, 2].Value.ToString();

                    if (int.TryParse(workSheet.Cells[i, 3].Value.ToString(), out warranty))
                    {
                        productViewModel.Waranty = warranty;

                    }

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString().Replace(",", ""), out originalPrice);
                    productViewModel.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString().Replace(",", ""), out price);
                    productViewModel.Price = price;

                    if (decimal.TryParse(workSheet.Cells[i, 6].Value.ToString(), out promotionPrice))
                    {
                        productViewModel.Promotion = promotionPrice;

                    }

                    productViewModel.Content = workSheet.Cells[i, 7].Value.ToString();
                    productViewModel.MetaKeywork = workSheet.Cells[i, 8].Value.ToString();

                    productViewModel.CategoryID = categoryId;

                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out status);
                    productViewModel.Status = status;

                    bool.TryParse(workSheet.Cells[i, 11].Value.ToString(), out showHome);
                    productViewModel.HomeFlag = showHome;

                    bool.TryParse(workSheet.Cells[i, 12].Value.ToString(), out isHot);
                    productViewModel.TopHot = isHot;

                    product.UpdateProduct(productViewModel);
                    listProduct.Add(product);
                }
                return listProduct;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request, string filter = null)
        {
            string fileName = string.Concat("Product_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = "/Reports";
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _productService.GetListProduct(filter).ToList();
                await ReportHelper.GenerateXls(data, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("ExportPdf")]
        public async Task<HttpResponseMessage> ExportPdf(HttpRequestMessage request, int id)
        {
            string fileName = string.Concat("Product" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".pdf");
            var folderReport = "/Reports";
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var template = File.ReadAllText(HttpContext.Current.Server.MapPath("/Assets/FileUpload/Product-Detail.html"));
                var replaces = new Dictionary<string, string>();
                var product = _productService.GetById(id);

                replaces.Add("{{Name}}", product.Name);
                replaces.Add("{{Price}}", product.Price.ToString("N0"));
                replaces.Add("{{Waranty}}", product.Waranty + " tháng");
                replaces.Add("{{Images}}", product.Images);
                template = template.Parse(replaces);

                await ReportHelper.GeneratePdf(template, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}