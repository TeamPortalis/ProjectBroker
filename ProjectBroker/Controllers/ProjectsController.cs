using ProjectBroker.Models.DBModel;
using ProjectBroker.Models.ViewModels;
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
            ProjectDetailsVM vm = new ProjectDetailsVM();

            var Project = DBManager.db.pr_project.Include("tm_team.s_student.p_person").Include("t_teacher.p_person")
                .Where(x => x.pr_id == id).First();
            vm.Project = Project;
            vm.TeamMembers = Project.tm_team.s_student.ToList();
            vm.Teacher = Project.t_teacher;
            return View("Details", vm);
        }
    }
}