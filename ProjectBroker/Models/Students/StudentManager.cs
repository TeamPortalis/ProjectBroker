using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;

namespace ProjectBroker.Models.Students
{
    public class StudentManager
    {
        //public string s_nr { get; set; }
        //public string s_address { get; set; }
        //public System.DateTime s_dob { get; set; }
        //public string s_d_department { get; set; }
        public static readonly string s_address_default = "Musterstraße 1";
        public static readonly System.DateTime s_dob_default = DateTime.Now;
        public static readonly string s_d_department_default = "D000000000";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s_nr_get"></param>
        /// <param name="s_address_get"></param>
        /// <param name="s_dob_get"></param>
        /// <param name="s_d_department_get"></param>
        /// <returns>Everything except for s_nr_get can be an empty string or null, if so, a default value will be set.</returns>
        public static s_student CreateStudent(string s_nr_get, string s_address_get, System.DateTime s_dob_get, string s_d_department_get)
        {
            var l = from p in DBManager.db.p_person
                    where p.p_id == s_nr_get
                    select p;
            
            if(s_nr_get == null || s_nr_get == ""||l.Count()<=0)
            {
                throw new ArgumentException("Invalid person id!");
            }
            var s_nr_set = s_nr_get;
            var s_address_set = (s_address_get == null || s_address_get == "") ? s_address_default : s_address_get;
            var s_dob_set = (s_dob_get == null) ? s_dob_default : s_dob_get;
            var s_d_department_set = (s_d_department_get == null || s_d_department_get == "")?s_d_department_default:s_d_department_get;
            var s = new s_student()
            {
                s_nr = s_nr_set,
                s_dob = s_dob_set,
                s_address = s_address_set,
                s_d_department = s_d_department_set
            };

            var r = DBManager.db.s_student.Add(s);
            DBManager.db.SaveChanges();

            return r;
        }

    }
}