using ProjectBroker.Models.DBModel;
using ProjectBroker.Models.Login;
using ProjectBroker.Models.Projects;
using ProjectBroker.Models.Teams;
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
        [Authorize]
        public ActionResult New()
        {
            var model = new NewProjectViewModel();
            model.AllProjectHostingEnvs = DBManager.db.phs_projhostingenv.ToList();
            model.AllProjectManagementEnvs = DBManager.db.pms_projmanagementenv.ToList();
            model.AllTeams = DBManager.db.tm_team.ToList();
            model.AllPossibleTeamMembers = DBManager.db.s_student.ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult New(string pr_name, string pr_desc, string pr_hosting_env, string pr_management_env, string pr_team, string pr_new_team_name, string[] pr_team_values)
        {
            //Add validation here or in view with metadata
            if (pr_name == null || pr_desc == null || pr_hosting_env == null || pr_management_env == null || pr_team == null)
                throw new ArgumentException("Null values presented");

            var team = pr_team;

            if(pr_team == "$NoTeam$")
            {
                if (pr_new_team_name == null || pr_team_values == null)
                    throw new ArgumentNullException();
                var newTeam = new tm_team() { tm_name = pr_new_team_name, tm_id = TeamManager.NextTeamID };
                DBManager.db.tm_team.Add(newTeam);
                DBManager.db.SaveChanges();
                team = newTeam.tm_id;
            }

            ProjectManager.CreateProject(pr_name,pr_desc,"","",pr_management_env,
                pr_hosting_env,team);

            //pr_project p = new pr_project()
            //{
            //    pr_name = pr_name_get,
            //    pr_desc = pr_desc_get,
            //    pr_pms_id = "PMS1",
            //    pr_phs_id = "PHV1",
            //    pr_t_id = "23221AB333",
            //    pr_tm_id = "TM1",
            //    pr_id = ProjectManager.GetNextProjectID()
            //};

            //DBManager.db.pr_project.Add(p);
            //DBManager.db.SaveChanges();

            /*Response.Write("Name:" + pr_name + "; Desc: " + pr_desc + "; HENV: " + pr_hosting_env + "; MENV: "+ pr_management_env);
            Response.Write("\nTeam Value: " + pr_team);
            if(pr_team == "$NoTeam$")
            {
                if (pr_new_team_name == null || pr_team_values == null)
                    throw new ArgumentException("Null values presented");

                Response.Write("New Team Name: " + pr_new_team_name);
                foreach(var i in pr_team_values)
                {
                    Response.Write("Value: " + i + "\n");
                }

            }*/
                
            return Redirect(Url.Action("Index", "Main"));
        }
    }
}