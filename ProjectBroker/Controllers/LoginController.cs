using ProjectBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBroker.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Login(string user, string password, bool remember_me)
        {
            ILoginManager l = new MockupLoginManager();
            var result = l.authenticate(MockupLoginManager.CreateLoginToken(user, password));
            Response.Write("Login " + (result ? "succeeded" : "failed"));
            return null;
        }
    }
}