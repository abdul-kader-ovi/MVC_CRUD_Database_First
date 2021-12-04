using MVCDatabaseFirstProject_TrainingAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCDatabaseFirstProject_TrainingAcademy.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public ActionResult PostLogin(tblUser obj)
        {
            using (var dbObj = new DBFirstTrainingDBEntities())
            {
                var count = dbObj.tblUsers.Where(u => u.UserName == obj.UserName && u.Password == obj.Password).Count();
                if (count <= 0)
                {
                    ViewBag.Message = "Invalid User";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(obj.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
            }

        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult PostSignUp(tblUser obj)
        {
            using (var dbObj = new DBFirstTrainingDBEntities())
            {
                bool isExist = !dbObj.tblUsers.Any(u => u.UserName == obj.UserName);
                if (isExist)
                {
                    dbObj.tblUsers.Add(obj);
                    dbObj.SaveChanges();
                    int count = dbObj.tblUsers.Count();
                    if (count == 1)
                    {
                        return RedirectToAction("CreateRole", "SuperAdmin");
                    }
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "UserName exist");
                    return View();
                }
            }
            //return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}