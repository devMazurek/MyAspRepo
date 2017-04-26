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
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}