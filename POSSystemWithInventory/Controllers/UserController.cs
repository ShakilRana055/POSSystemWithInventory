using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSSystemWithInventory.EntityModel;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;

namespace POSSystemWithInventory.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork context;

        public UserController(IUnitOfWork appDbContext)
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
            return View();
        }
    }
}
