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
    public interface ISlideService
    {
        IEnumerable<Slide> GetAll();
        Slide Delete(int id);
        Slide Create(Slide slide);

        void Update(Slide slide);
        Slide GetById(int id);
        void SaveChange();
    }
    public class SlideService: ISlideService
    {
        ISlideRepository _slideRepository;
        IUnitOfWork _unitOfWork;
        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public Slide Create(Slide slide)
        {
           return _slideRepository.Add(slide);
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
           return _slideRepository.GetAll();
        }

        public Slide GetById(int id)
        {
           return _slideRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}
