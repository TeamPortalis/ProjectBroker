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

        public static string GetNextProjectID()
        {
            var projectIDs = (from p in DBManager.db.pr_project
                            orderby p.pr_id
                            select p.pr_id);
            var projectID = projectIDs.LastOrDefault();
            var suffix = projectID.Substring(2);
            var num = Int32.Parse(suffix);
            var retstr = "PID" + (num++);
            return retstr;
        }

    }
}