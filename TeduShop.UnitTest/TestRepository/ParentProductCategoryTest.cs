using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;

namespace TeduShop.UnitTest.TestRepository
{
    [TestClass]
   public class ParentProductCategoryTest
    {
        IDbFactory _dbFactory;
        IUnitOfWork _unitOfWork;
        IParentProductCategoryRepository _productRepository;

        [TestInitialize]
        public void testInitialize()
        {
            _dbFactory = new DbFactory();
            _productRepository = new ParentProductCategoryRepository(_dbFactory);
            _unitOfWork = new UnitOfWork(_dbFactory);

        }
        [TestMethod]
        public void GetAll()
        {

          var result=  _productRepository.GetMutileParent(1);
        }
        [TestMethod]
        public void Product_Repository_Delete()
        {
            var model = _productRepository.GetSingleById(10);
            _productRepository.Delete(model);
            _unitOfWork.Commit();

            Assert.AreEqual(model.ID, 10);
        }
    }
}
