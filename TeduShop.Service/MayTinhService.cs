using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IMayTinhService
    {

        IEnumerable<MayTinh> GetAll();
        IEnumerable<MayTinh> GetAll(string keyWord);
        MayTinh Delete(int id);
        MayTinh Create(MayTinh maytinh);

        void Update(MayTinh maytinh);
        MayTinh GetById(int id);
        void SaveChange();
    }
    public class MayTinhService :IMayTinhService
    {
        IMayTinhRepository _maytinhRepository;
        IUnitOfWork _unitOfWork;
        public MayTinhService(IMayTinhRepository maytinhRepository, IUnitOfWork unitOfWork)
        {
            this._maytinhRepository = maytinhRepository;
            this._unitOfWork = unitOfWork;
        }

        public MayTinh Create(MayTinh maytinh)
        {
            return _maytinhRepository.Add(maytinh);
        }

        public MayTinh Delete(int id)
        {
            return _maytinhRepository.Delete(id);
        }

        public IEnumerable<MayTinh> GetAll()
        {
            return _maytinhRepository.GetAll();
        }

        public IEnumerable<MayTinh> GetAll(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return _maytinhRepository.GetAll();
            }
            else
            {
                return _maytinhRepository.GetMulti(x => x.Name.Contains(keyWord)|| x.Desciption.Contains(keyWord));
            }
        }

        public MayTinh GetById(int id)
        {
            return _maytinhRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(MayTinh maytinh)
        {
            _maytinhRepository.Update(maytinh);
        }
    }
}
