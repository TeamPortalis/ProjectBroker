using ProjectBroker.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models.Projects
{
    public class ProjectManager
    {
        public static readonly string pr_name_default = "Default Project Name";
        public static readonly string pr_desc_default = "Default Description";
        public static readonly string pr_image_default = "";
        public static readonly string pr_t_id_default = "THE0000001";
        public static readonly string pr_pms_id_default = "PMS1";
        public static readonly string pr_phs_id_default = "PHV1";
        public static readonly string pr_tm_id_default = "TM1";


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
                            select p.pr_id).ToList();
            var projectID = projectIDs[projectIDs.Count - 1];
            var suffix = projectID.Substring(3);
            var num = Int32.Parse(suffix);
            var retstr = "PID" + (num+1);
            return retstr;
        }

        public static pr_project DeleteProject(string P_ID)
        {
            if (P_ID == null || P_ID == "")
                throw new ArgumentException("P_ID Null in DeleteProject");
            var l = (from p in DBManager.db.pr_project
                     select p).ToList();
            var f = l.First();
            var i = l.RemoveAll(x => x.pr_id == P_ID);

            if (i == 0)
                throw new ArgumentException("No matching Projects!");
            return f;
        }

        public static pr_project CreateProject(string pr_name_get, string pr_desc_get, string pr_image_get, string pr_t_id_get, string pr_pms_id_get, string pr_phs_id_get, string pr_tm_id_get)
        {
            if (pr_name_get == null)
                throw new ArgumentException("invalid pr_name");

            //Add default image
            var pr_image_set = (pr_image_get == null) ? ProjectManager.pr_image_default : pr_image_get;
            var pr_name_set = (pr_name_get == "") ? ProjectManager.pr_name_default : pr_name_get;
            var pr_desc_set = (pr_desc_get == null || pr_desc_get == "") ? ProjectManager.pr_desc_default : pr_desc_get;
            var pr_id_set = ProjectManager.GetNextProjectID();
            var pr_t_id_set = (pr_tm_id_get == null || pr_tm_id_get == "") ? ProjectManager.pr_tm_id_default : pr_tm_id_get;
            var pr_pms_id_set = (pr_pms_id_get == null || pr_pms_id_get == "") ? ProjectManager.pr_pms_id_default : pr_pms_id_get;
            var pr_phs_id_set = (pr_phs_id_get == null || pr_phs_id_get == "") ? ProjectManager.pr_phs_id_default : pr_phs_id_get;
            var pr_tm_id_set = (pr_tm_id_get == null || pr_tm_id_get == "") ? ProjectManager.pr_tm_id_default : pr_tm_id_get;

            pr_project p = new pr_project()
            {
                pr_id = pr_id_set,
                pr_desc = pr_desc_set,
                pr_name = pr_name_set,
                pr_image = pr_image_set,
                pr_phs_id = pr_phs_id_set,
                pr_pms_id = pr_pms_id_set,
                pr_tm_id = pr_tm_id_set,
                pr_t_id = pr_t_id_set
            };

            DBManager.db.pr_project.Add(p);
            DBManager.db.SaveChanges();

            return p;

        }

        public static pr_project UpdateProject(string P_ID, string pr_name_get, string pr_desc_get, string pr_image_get, string pr_t_id_get, string pr_pms_id_get, string pr_phs_id_get, string pr_tm_id_get)
        {
            var l = DeleteProject(P_ID);
            var f = CreateProject(pr_name_get, pr_desc_get, pr_image_get, pr_t_id_get, pr_pms_id_get, pr_phs_id_get, pr_tm_id_get);
            if (f == null)
            {
                DBManager.db.pr_project.Add(l);
                DBManager.db.SaveChanges();
                throw new ArgumentException("Project could not be created in the update process!");
            }
            return f;
        }

    }
}