using System;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ICommonService
    {
        Contact GetContact();
        void CreateFeedback(Feedback feedback);
        void SaveChange();
    }
    public class CommonService : ICommonService
    {
        IContactRepository _contactRepository;
        IFeedbackRepository _feedbackRepository;
        IUnitOfWork _unitOfWork;

        public CommonService(IContactRepository contactRepository, IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._contactRepository = contactRepository;
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateFeedback(Feedback feedback)
        {
            _feedbackRepository.Add(feedback);
        }

        public Contact GetContact()
        {
            return _contactRepository.GetSingleByCondition(x => x.Status);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
