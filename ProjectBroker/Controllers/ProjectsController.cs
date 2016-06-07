using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBroker.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        [Authorize]
        public ActionResult Index(string id)
        {
               
            return View();
        }
    }
}