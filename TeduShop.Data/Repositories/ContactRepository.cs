using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {

    }
    public class ContactRepository:RepositoryBase<Contact> , IContactRepository
    {
        public ContactRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
