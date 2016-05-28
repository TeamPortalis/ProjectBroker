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
                return Redirect(Url.Action(actionName: "Index", controllerName: "Main"));
            }else
            {
                return View("Index");
            }
        }


        [HttpPost]
        public ActionResult Logout(string urlToRedirectTo)
        {
            FormsAuthentication.SignOut();
            if (Url.IsLocalUrl(urlToRedirectTo))
            {
                return Redirect(urlToRedirectTo);
            } else
            {
                return Redirect(Url.Action(actionName: "Index", controllerName: "Main"));
            }
        }
    }
}