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
            if(Session["UName"] != null)
            {
                return Redirect(Url.Action(actionName: "Index", controllerName: "Main"));
            }
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Login(string user, string password, bool remember_me)
        {
            ILoginManager l = new StandaloneLoginManage();
            IAuthTokenFactory<StandaloneAuthParams> para = (IAuthTokenFactory<StandaloneAuthParams>)l;
            var authToken = para.CreateAuthToken(StandaloneAuthParams.CreateByUserPass(user, password));
            var result = l.authenticate(authToken);
            if (result)
            {
                if (remember_me)
                    Session["UName"] = user;
                return Redirect(Url.Action(actionName: "Index", controllerName: "Main"));
            }else
            {
                return View("Index");
            }
        }
    }
}