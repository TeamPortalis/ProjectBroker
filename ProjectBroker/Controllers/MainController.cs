using ProjectBroker.Models.DBModel;
using ProjectBroker.Models.Login;
using ProjectBroker.Models.Projects;
using ProjectBroker.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectBroker.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        [Authorize]
        public ActionResult Index()
        {

            var currentUserName = HttpContext.User.Identity.Name;
            var user = LoginUtils.UserForLogin(currentUserName);

            IEnumerable<pr_project> projectsForCurrentUser = ProjectManager.GetAllProjectOfCurrentUser(user);

            var vm = new MainViewViewModel() { ActiveUser = user, CurrentProjects = projectsForCurrentUser,
                RecommendedProjects = projectsForCurrentUser};

            return View(vm );
        }

        // GET: New
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult New(string pr_name_get, string pr_desc_get, string pr_pms_string_get, string pr_phs_string_get)
        {
            //Add validation here or in view with metadata
            pr_project p = new pr_project()
            {
                pr_name = pr_name_get,
                pr_desc = pr_desc_get,
                pr_pms_id = pr_pms_string_get,
                pr_phs_id = pr_phs_string_get,
                //pr_t_id = "",
                //pr_tm_id = "",
                pr_id = ProjectManager.GetNextProjectID()
            };

            DBManager.db.pr_project.Add(p);
            DBManager.db.SaveChanges();
            return View();
    }
    }
}