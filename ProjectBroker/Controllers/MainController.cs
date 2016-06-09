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
        // Get the main page showing the logged in user and the projects he/she is currently working on.
        [Authorize]
        public ActionResult Index()
        {

            var currentUserName = HttpContext.User.Identity.Name; //Get currently logged in username
            var user = LoginUtils.UserForLogin(currentUserName); //Find matching user.

            IEnumerable<pr_project> projectsForCurrentUser = ProjectManager.GetAllProjectOfCurrentUser(user); //Get Projects

            var vm = new MainViewViewModel() { ActiveUser = user, CurrentProjects = projectsForCurrentUser,
                RecommendedProjects = projectsForCurrentUser}; //Prepare ViewModel

            return View(vm); //Hand over model and View
        }

        // GET: New
        //Prepare and load the Project Creation View
        [Authorize]
        public ActionResult New()
        {
            //Prepare ViewModel
            var model = new NewProjectViewModel();
            model.AllProjectHostingEnvs = DBManager.db.phs_projhostingenv.ToList();
            model.AllProjectManagementEnvs = DBManager.db.pms_projmanagementenv.ToList();
            model.AllTeams = DBManager.db.tm_team.ToList();
            model.AllPossibleTeamMembers = DBManager.db.s_student.ToList();

            //Hand over to view 
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult New(string pr_name, string pr_desc, string pr_hosting_env, string pr_management_env, string pr_team, string pr_new_team_name, string[] pr_team_values)
        {
            //Add validation here or in view with metadata
            if (pr_name == null || pr_desc == null || pr_hosting_env == null || pr_management_env == null || pr_team == null)
                throw new ArgumentException("Null values presented");

            //Declare team variable holding the id of the team and assign the first value.
            var team = pr_team;

            if(pr_team == "$NoTeam$") //If $NoTeam$ (Value indicating that a new Team should be created) is present, create new Team
            {
                //If team creation parameters are null at this stage, something went wrong.
                if (pr_new_team_name == null || pr_team_values == null)
                    throw new ArgumentNullException();
                
                //Create new Team with EF and assign team member.s
                var newTeam = new tm_team() { tm_name = pr_new_team_name, tm_id = TeamManager.NextTeamID };
                DBManager.db.tm_team.Add(newTeam);
                foreach (var p in pr_team_values)
                {
                    var teamMember = DBManager.db.s_student.Where(x => p == x.s_nr).First();
                    newTeam.s_student.Add(teamMember);
                }
                DBManager.db.SaveChanges(); //Save it 
                team = newTeam.tm_id;       //and assisng team the id of the new Team
            }

            ProjectManager.CreateProject(pr_name, pr_desc, "", "", pr_management_env,
                pr_hosting_env, team); //Create the project via the ProjectManager for standardized Data access.
            DBManager.db.SaveChanges();
             
            return Redirect(Url.Action("Index", "Main")); //Redirect to main page.
        }
    }
}