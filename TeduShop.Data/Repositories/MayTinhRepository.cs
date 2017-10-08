using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IMayTinhRepository:IRepository<MayTinh>
    {

    }
    public class MayTinhRepository:RepositoryBase<MayTinh>,IMayTinhRepository
    {
        public MayTinhRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
