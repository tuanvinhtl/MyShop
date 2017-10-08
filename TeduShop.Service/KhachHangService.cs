using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IKhachHangService
    {
        IEnumerable<KhachHang> GetAll();
        IEnumerable<KhachHang> GetAll(string keyWord);
        KhachHang Delete(int id);
        KhachHang Create(KhachHang khachhang);

        void Update(KhachHang khachhang);
        KhachHang GetById(int id);
        void SaveChange();
    }
    public class KhachHangService: IKhachHangService
    {
        IKhachHangRepository _khachHangRepository;
        IUnitOfWork _unitOfWork;
        public KhachHangService(IKhachHangRepository khachHangRepository, IUnitOfWork unitOfWork)
        {
            this._khachHangRepository = khachHangRepository;
            this._unitOfWork = unitOfWork;
        }

        public KhachHang Create(KhachHang khachhang)
        {
            return _khachHangRepository.Add(khachhang);
        }

        public KhachHang Delete(int id)
        {
            return _khachHangRepository.Delete(id);
        }

        public IEnumerable<KhachHang> GetAll()
        {
            return _khachHangRepository.GetAll();
        }

        public IEnumerable<KhachHang> GetAll(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return _khachHangRepository.GetAll();
            }
            else
            {
                return _khachHangRepository.GetMulti(x => x.Name.Contains(keyWord) || x.PhoneNumber.Contains(keyWord));
            }
        }

        public KhachHang GetById(int id)
        {
            return _khachHangRepository.GetSingleById(id);
        }


        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(KhachHang khachhang)
        {
            _khachHangRepository.Update(khachhang);
        }
    }
}
