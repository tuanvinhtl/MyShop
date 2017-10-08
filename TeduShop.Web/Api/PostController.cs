using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
 [RoutePrefix("api/post")]
    public class PostController : ApiControllerBase
    {
        private IPostService _postService;
        public PostController(IErrorService errorService, IPostService postService) : base(errorService)
        {
            this._postService = postService;
        }
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, Post post)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                     var en=_postService.Add(post);
                    _postService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, en);
                }
                return response;
            });
        }
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var listResult = _postService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, listResult);
                return response;
            });
        }

    }
}
