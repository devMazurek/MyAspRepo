using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Models;
using AHP2.Auth;

namespace AHP2.Controllers
{
    [MyAuth]
    public class UserController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid && user != null)
            {
                if (_ormContext.UsersContext.Where(u => u.EmailAdress == user.EmailAdress).First() == null)
                {
                    _ormContext.UsersContext.Add(user);
                    _ormContext.SaveChanges();
                    return RedirectToAction("LogIn");
                }
                else
                {
                    ModelState.AddModelError("notUniqueEmail", "This email address already exist in data base");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(User user)
        {
            var usr = _ormContext.UsersContext.Where(u => u.EmailAdress == user.EmailAdress && u.Password == user.Password).FirstOrDefault();
            if (usr != null)
            {
                Session["User"] = usr;
                return RedirectToAction("IndexProject", "Ahp");
            }
            else
            {
                ViewBag.Message = "Email or password is wrong!";
            }
            return View();
        }


        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("LogIn", "LogIn");
        }

        public ActionResult LoggedIn()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
    }
}