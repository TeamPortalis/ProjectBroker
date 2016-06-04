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
    }
}