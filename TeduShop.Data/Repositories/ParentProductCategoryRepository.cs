using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IParentProductCategoryRepository : IRepository<ParentProductCategory>
    {
        IEnumerable<ProductCategory> GetMutileParent(int id);

    }
    public class ParentProductCategoryRepository:RepositoryBase<ParentProductCategory>,IParentProductCategoryRepository
    {
        public ParentProductCategoryRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }

        public IEnumerable<ProductCategory> GetMutileParent(int id)
        {
            var query =  from p in DbContext.ParentProductCategories
                         join c in DbContext.ProductCategories 
                         on p.ID equals c.ProductCategoryParentID where p.ID==id  select c;
            return query;
        }
    }
}
