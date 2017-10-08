using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyWord);
        Product Delete(int id);
        Product Create(Product product);
        IEnumerable<Product> GetListProduct(string filter);
        void Update(Product product);
        Product GetById(int id);
        IEnumerable<Product> GetListProductByCategoryId(int id);
        IEnumerable<Product> GetListProductByPaginatonSet(int categoryId, int page,int pageSize,out int totalRow);
        IEnumerable<Product> GetProductTrendingTop(int top);
        IEnumerable<Product> GetProductTrendingBot(int bot);

        IEnumerable<Tag> GetListTagByProductId(int productTagId);
        void SaveChange();
    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;
        private IProductTagRepository _productTagRepository;
        private ITagRepository _tagRepository;
        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
        }
        public Product Create(Product product)
        {
            var result = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = Common.StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.TagID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.TagID = tagId;
                        tag.Name = tags[i];
                        tag.Type = Common.CommonConstain.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return result;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                return _productRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Descryption.Contains(keyWord));
            }
            else
            {
                return _productRepository.GetAll();
            }
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = Common.StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.TagID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.TagID = tagId;
                        tag.Name = tags[i];
                        tag.Type = Common.CommonConstain.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            
        }

        public IEnumerable<Product> GetListProduct(string filter)
        {
            IEnumerable<Product> query;
            if (!string.IsNullOrEmpty(filter))
            {
                query = _productRepository.GetMulti(x => x.Name.Contains(filter));
            }
            else
            {
                query = _productRepository.GetAll();
            }
            return query;
        }

        public IEnumerable<Product> GetProductTrendingTop(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.TopHot == true).Take(top).OrderBy(x=>x.DisplayOrder);
        }

        public IEnumerable<Product> GetProductTrendingBot(int bot)
        {
            return _productRepository.GetMulti(x => x.Status).OrderBy(x => x.DisplayOrder).Take(bot);
        }

        public IEnumerable<Product> GetListProductByCategoryId(int id)
        {
            return _productRepository.GetMulti(x => x.Status && x.CategoryID == id).OrderBy(x=>x.CreatedDate);
        }

        public IEnumerable<Product> GetListProductByPaginatonSet(int categoryId, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Tag> GetListTagByProductId(int productTagId)
        {
           return _productTagRepository.GetMulti(x => x.ProductID == productTagId, new string[] { "Tag" }).Select(y => y.Tag);
        }
    }
}
