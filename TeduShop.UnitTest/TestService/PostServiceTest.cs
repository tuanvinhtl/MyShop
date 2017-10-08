using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.UnitTest.TestService
{
    [TestClass]
    public class PostServiceTest
    {
        private Mock<IPostRepository> _mockPostRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostService _postService;
        private List<Post> _listPost;
        [TestInitialize]
        public void Initialize()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postService = new PostService(_mockPostRepository.Object, _mockUnitOfWork.Object);
            _listPost = new List<Post>()
            {
                new Post() {ID=1,Name="Test",Alias="Test1",Status=true },
                new Post() {ID=2,Name="Test1",Alias="Test12",Status=false },
                new Post() {ID=3,Name="Test2",Alias="Test13",Status=true },
                new Post() {ID=4,Name="Test3",Alias="Test14",Status=true },
            };
        }
        [TestMethod]
        public void Post_GetAll_Test()
        {
            _mockPostRepository.Setup(x => x.GetAll(null)).Returns(_listPost);

            var result = _postService.GetAll() as List<Post>;

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
        }
        [TestMethod]
        public void Post_Create_Test()
        {

        }
    }
}
