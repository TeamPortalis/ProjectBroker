using ProjectBroker.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models.Projects
{
    public class ProjectManager
    {

        public static IEnumerable<pr_project> GetAllProjectOfCurrentUser(p_person user)
        {

            var projects = DBManager.db.pr_project;
            var userTeams = DBManager.db.tm_team.Where(x => x.s_student.Any(r => r.s_nr == user.p_id));
            var result = projects.Where(x => userTeams.Any(p => p.tm_id == x.tm_team.tm_id));
            return result.ToList();
        }

    }
}