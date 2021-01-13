using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.ConstantAndHelpers;
using POSSystemWithInventory.Models;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class SettingController : Controller
    {
        private readonly IUnitOfWork context;

        public SettingController(IUnitOfWork unitOfWork)
        {
            context = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Send Email
        public IActionResult SendEmailToSupplier()
        {
            var supplier = context.Supplier.GetAll().ToList();
            SettingVM setting = new SettingVM()
            {
                SupplierItem = POSHelper.Supplier(supplier),
            };
            return View(setting);
        }

        public IActionResult SendEmailToCustomer()
        {
            var customerList = context.Customer.GetAll().ToList();
            SettingVM setting = new SettingVM()
            {
                SupplierItem = POSHelper.Customer(customerList),
            };
            return View(setting);
        }

        [HttpPost]
        public IActionResult SendEmailToSupplier(SettingVM setting)
        {
            try
            {
                foreach (var item in setting.Information)
                {
                    var supplierInfo = context.Customer.Find(x => x.Id == item).FirstOrDefault();
                    if (supplierInfo != null && supplierInfo.Email != "")
                    {
                        MailMessage message = new MailMessage(
                                                    "shakil7055@gmail.com",
                                                    supplierInfo.Email,
                                                    setting.Subject,
                                                    setting.Message
                                                    );
                        message.IsBodyHtml = false;
                        SmtpClient smtpClient = new SmtpClient("Smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("shakil7055@gmail.com", "shakilrana7055");
                        smtpClient.Send(message);
                    }
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult SendEmailToCustomer(SettingVM setting)
        {
            try
            {
                foreach (var item in setting.Information)
                {
                    var supplierInfo = context.Supplier.Find(x => x.Id == item).FirstOrDefault();
                    if (supplierInfo != null && supplierInfo.Email != "")
                    {
                        MailMessage message = new MailMessage(
                                                    "shakil7055@gmail.com",
                                                    supplierInfo.Email,
                                                    setting.Subject,
                                                    setting.Message
                                                    );
                        message.IsBodyHtml = false;
                        SmtpClient smtpClient = new SmtpClient("Smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("shakil7055@gmail.com", "shakilrana7055");
                        smtpClient.Send(message);
                    }
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
                throw ex;
            }

        }
        #endregion

        #region Password Change
        public IActionResult ChangePassword()
        {
            UserLoginVM userLoginVM = new UserLoginVM();
            return View(userLoginVM);
        }
        [HttpPost]
        public IActionResult ChangePassword(UserLoginVM userLogin)
        {
            var user = context.User.Find(item => item.HasLogged).FirstOrDefault();
            if(user != null)
            {
                user.Password = userLogin.NewPassword;
                context.Save();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        #endregion
    }
}
