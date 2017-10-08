using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IChiTietSuaChuaService
    {
        IEnumerable<ChiTietSuaChua> GetAll();
        IEnumerable<ChiTietSuaChua> GetAll(int keyWordID);
        ChiTietSuaChua Delete(int id);
        ChiTietSuaChua Create(ChiTietSuaChua chitietsuachua);

        void Update(ChiTietSuaChua chitietsuachua);
        ChiTietSuaChua GetById(int id);
        void SaveChange();
    }
    public class ChiTietSuaChuaService : IChiTietSuaChuaService
    {
        IChiTietSuaChuaRepository _chitietSuaChuaRepository;
        IUnitOfWork _unitOfWork;
        public ChiTietSuaChuaService(IChiTietSuaChuaRepository chitietSuaChuaRepository, IUnitOfWork unitOfWork)
        {
            this._chitietSuaChuaRepository = chitietSuaChuaRepository;
            this._unitOfWork = unitOfWork;
        }

        public ChiTietSuaChua Create(ChiTietSuaChua chitietsuachua)
        {
            return _chitietSuaChuaRepository.Add(chitietsuachua);
        }

        public ChiTietSuaChua Delete(int id)
        {
            return _chitietSuaChuaRepository.Delete(id);
        }

        public IEnumerable<ChiTietSuaChua> GetAll()
        {
            return _chitietSuaChuaRepository.GetAll();
        }

        public IEnumerable<ChiTietSuaChua> GetAll(int keyWordID)
        {
            if (keyWordID==0)
            {
                return _chitietSuaChuaRepository.GetAll();
            }
            else
            {
                return _chitietSuaChuaRepository.GetMulti(x => x.IDMayTinh == keyWordID);
            }
        }

        public ChiTietSuaChua GetById(int id)
        {
            return _chitietSuaChuaRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(ChiTietSuaChua chitietsuachua)
        {
            _chitietSuaChuaRepository.Update(chitietsuachua);
        }
    }
}
