using System;
using System.Collections;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Linq;

namespace TeduShop.Data.Repositories
{
    public interface ITagRepository:IRepository<Tag>
    {
        IEnumerable<Tag> GetListTagByProductId(int id);

    }
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            var query = from t in DbContext.Tags join pt 
                        in DbContext.ProductTags on t.TagID 
                        equals pt.TagID where pt.ProductID == id
                        select t;
            return query;
        }
    }

}
