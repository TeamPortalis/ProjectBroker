using ProjectBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectBroker.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
             return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string user, string password, bool remember_me)
        {
            ILoginManager l = new StandaloneLoginManage();
            IAuthTokenFactory<StandaloneAuthParams> para = (IAuthTokenFactory<StandaloneAuthParams>)l;
            var authToken = para.CreateAuthToken(StandaloneAuthParams.CreateByUserPass(user, password));
            var result = l.authenticate(authToken);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(user, remember_me);
                return Redirect(Url.Action("Index", "Main"));
            }else
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Logout(string urlToRedirectTo)
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Index", "Main"));
        }
    }
}