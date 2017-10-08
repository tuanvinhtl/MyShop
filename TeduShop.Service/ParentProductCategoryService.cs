using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IParentProductCategoryService
    {
        IEnumerable<ParentProductCategory> GetAll();
        ParentProductCategory Delete(int id);
        ParentProductCategory Create(ParentProductCategory parentProductCategory);
        void Update(ParentProductCategory parentProductCategory);
        ParentProductCategory GetById(int id);
        IEnumerable<ProductCategory> GetParentProductCategory(int id);
        void SaveChange();
    }
    public class ParentProductCategoryService : IParentProductCategoryService
    {
        IParentProductCategoryRepository _parentProductCategory;
        IUnitOfWork _unitOfWork;

        public ParentProductCategoryService(IParentProductCategoryRepository parentProductCategory, IUnitOfWork unitOfWork)
        {
            this._parentProductCategory = parentProductCategory;
            this._unitOfWork = unitOfWork;

        }

        public ParentProductCategory Create(ParentProductCategory parentProductCategory)
        {
            return _parentProductCategory.Add(parentProductCategory);
        }

        public ParentProductCategory Delete(int id)
        {
            return _parentProductCategory.Delete(id);
        }

        public IEnumerable<ParentProductCategory> GetAll()
        {
            return _parentProductCategory.GetAll();
        }

        public ParentProductCategory GetById(int id)
        {
            return _parentProductCategory.GetSingleById(id);
        }

        public IEnumerable<ProductCategory> GetParentProductCategory(int id)
        {
            return _parentProductCategory.GetMutileParent(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
        public void Update(ParentProductCategory parentProductCategory)
        {
            _parentProductCategory.Update(parentProductCategory);
        }
    }
}
