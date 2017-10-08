using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using System.Linq;

namespace TeduShop.Service
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyWord);
        ProductCategory Delete(int id);
        ProductCategory Create(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        ProductCategory GetById(int id);
        IEnumerable<ProductCategory> GetProductCategoryForMan(int top);
        IEnumerable<ProductCategory> GetProductCategoryForWoman(int top);
        IEnumerable<ProductCategory> GetProductCategoryForKid(int top);
        void SaveChange();



    }
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }
        public ProductCategory Create(ProductCategory productCategory)
        {
            return _productCategoryRepository.Add(productCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll(new string[] { "ParentProductCategory" });
        }

        public IEnumerable<ProductCategory> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                return _productCategoryRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Descryption.Contains(keyWord));
            }
            else
            {
                return _productCategoryRepository.GetAll();
            }
        }

        public ProductCategory GetById(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<ProductCategory> GetProductCategoryForKid(int top)
        {
            return _productCategoryRepository.GetMulti(x => x.ForKid == true && x.Status).Take(top);
        }

        public IEnumerable<ProductCategory> GetProductCategoryForMan(int top)
        {
           return _productCategoryRepository.GetMulti(x => x.ForMan == true && x.Status).Take(top);
            
        }

        public IEnumerable<ProductCategory> GetProductCategoryForWoman(int top)
        {
            return _productCategoryRepository.GetMulti(x => x.ForWomen == true && x.Status).Take(top);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory productCategory)
        {
            _productCategoryRepository.Update(productCategory);
        }
    }
}
