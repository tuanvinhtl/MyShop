using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;
using BotDetect.Web.Mvc;
using TeduShop.Common;

namespace TeduShop.Web.Controllers
{
    public class ContactController : Controller
    {
        ICommonService _commonService;

        public ContactController(ICommonService commonService)
        {
            this._commonService = commonService;

        }

        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Contact = GetContact();
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Sai mã xác nhận!")]
        public ActionResult Feedback(FeedbackViewModel feedbackVM)
        {
            if (ModelState.IsValid)
            {
                Feedback feedback = new Feedback();
                feedback.UpdateFeedback(feedbackVM);
                _commonService.CreateFeedback(feedback);
                _commonService.SaveChange();
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/SentEmail.html"));
                content = content.Replace("{{Name}}", feedbackVM.Name);
                content = content.Replace("{{Email}}", feedbackVM.Email);
                content = content.Replace("{{Message}}", feedbackVM.Message);
                var emailAdmin = ConfigHelper.GetByKey("EmailAdmin");
                MailHelper.SendMail(emailAdmin, "Thông tin phản hồi từ Tuấn Vinh Web", content);
                ViewData["Feedback"] = "Success";
            }
            else
            {
                ViewData["Feedback"] = "Failed";

            }
            ViewBag.Contact = GetContact();

            return View("index", feedbackVM);
        }
        private ContactViewModel GetContact()
        {
            var model = _commonService.GetContact();
            var mapper = Mapper.Map<Contact, ContactViewModel>(model);
            return mapper;
        }
    }
}