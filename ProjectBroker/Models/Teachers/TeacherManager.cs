using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBroker.Models.DBModel;
using System.Web;

namespace ProjectBroker.Models.Teachers
{
    public class TeacherManager
    {
        private static object s_d_department_default;

        public static t_teacher CreateTeacher(string t_id_get)
        {
            var l = from p in DBManager.db.p_person
                    where p.p_id == t_id_get
                    select p;

            if (t_id_get == null || t_id_get == "" || l.Count() <= 0)
            {
                throw new ArgumentException("Invalid person id!");
            }
            var t_id_set = t_id_get;
            var t = new t_teacher()
            {
                t_id = t_id_set
            };

            var r = DBManager.db.t_teacher.Add(t);
            DBManager.db.SaveChanges();

            return r;
        }

    }
}