using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.Models;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork context;

        public LoginController(IUnitOfWork appDbContext)
        {
            context = appDbContext;
        }

        #region Private Helper
        private void CreateUser()
        {
            User user = new User()
            {
                Name = "Admin",
                UserName = "admin@gmail.com",
                Password = "1234",
                Email = "admin@gmail.com",
            };
            context.User.Add(user);
            context.Save();
        }
        #endregion

        public IActionResult Index()
        {
            var getUser = context.User.GetAll().ToList();
            if(getUser.Count == 0)
            {
                CreateUser();
            }
            UserLoginVM userLoginVM = new UserLoginVM();
            return View(userLoginVM);
        }
        [HttpPost]
        public IActionResult Index(UserLoginVM userLoginVM)
        {
            if (ModelState.IsValid)
            {
                var getUser = context.User.Find(x => x.Email == userLoginVM.Email && x.Password == userLoginVM.Password).FirstOrDefault();
                if(getUser != null)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    userLoginVM.ErrorMessage = "Username or Password is incorrect";
                    return View(userLoginVM);
                }
            }
            else
            {
                userLoginVM.ErrorMessage = "Username or Password is incorrect";
                return View(userLoginVM);
            }
        }
    }
}
