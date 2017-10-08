using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using System.Linq;
namespace TeduShop.UnitTest.TestRepository
{
    [TestClass]
    public class ProductRepositoryTest
    {
        IDbFactory _dbFactory;
        IUnitOfWork _unitOfWork;
        IProductRepository _productRepository;
        ITagRepository _tagRepository;

        [TestInitialize]
        public void testInitialize()
        {
            _dbFactory = new DbFactory();
            _productRepository = new ProductRepository(_dbFactory);
            _unitOfWork = new UnitOfWork(_dbFactory);
            _tagRepository = new TagRepository(_dbFactory);

        }
        [TestMethod]
        public void Product_Repository_Create()
        {
            Product product = new Product();
            product.Name = "test 1";
            product.Alias = "ali-as";
            product.CategoryID = 1;
            product.Status = true;
            _productRepository.Add(product);
            _unitOfWork.Commit();

            Assert.AreEqual(product.ID, product.ID++);

        }
        [TestMethod]
        public void Product_Repository_Delete()
        {
            var model= _productRepository.GetSingleById(10);
            _productRepository.Delete(model);
            _unitOfWork.Commit();

            Assert.AreEqual(model.ID, 10);
        }
        [TestMethod]
        public void Product_Get_Tags()
        {
            var model = _tagRepository.GetListTagByProductId(62);

            Assert.AreEqual(model.Count(), 9);
        }
    }
}
