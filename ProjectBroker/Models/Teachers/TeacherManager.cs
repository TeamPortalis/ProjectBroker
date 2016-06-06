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
                throw new ArgumentException("Invalid teacher id!");
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

        public static IEnumerable<t_teacher> GetAllTeachers()
        {
            var l = (from i in DBManager.db.t_teacher
                     select i).ToList();
            return l;
        }

        public static t_teacher DeleteTeacher(string t_id_get)
        {
            if (t_id_get == null || t_id_get == "")
                throw new ArgumentException("t_id Null in Delete Teacher");
            var l = (from p in DBManager.db.t_teacher
                     select p).ToList();
            var f = l.First();
            var i = l.RemoveAll(x => x.t_id == t_id_get);
            if (i == 0)
                throw new ArgumentException("No matching Teachers in delete!");
            DBManager.db.SaveChanges();
            return f;
        }

        public static t_teacher UpdateStudent(string t_id_get)
        {
            var l = DeleteTeacher(t_id_get);
            var f = CreateTeacher(t_id_get);
            if (f == null)
            {
                DBManager.db.t_teacher.Add(l);
                DBManager.db.SaveChanges();
                throw new ArgumentException("Teacher could not be created in the update process!");
            }
            return f;
        }

    }
}