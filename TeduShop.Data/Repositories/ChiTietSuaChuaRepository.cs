using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IChiTietSuaChuaRepository : IRepository<ChiTietSuaChua>
    {

    }
    public class ChiTietSuaChuaRepository:RepositoryBase<ChiTietSuaChua>,IChiTietSuaChuaRepository
    {
        public ChiTietSuaChuaRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
    }
}
