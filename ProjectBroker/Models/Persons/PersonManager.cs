using System;
using ProjectBroker.Models.DBModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models.Persons
{
    public class PersonManager
    {
        public static readonly string p_fname_default = "Maximustian";
        public static readonly string p_lname_default = "Mustermann";
        public static readonly string p_email_default = "Muster@Mustermail-com";
        public static readonly string p_phonenr_default = "000000000000";
        public static readonly string p_image_default = "https://abs.twimg.com/sticky/default_profile_images/default_profile_3_400x400.png";
        /// <summary>
        /// Create a person, if p_id_get is an empty string a random value not already a primary key will be assigned, if anything else is an empty string then a default value is assigned.
        /// </summary>
        /// <param name="p_id_get">can be empty string, random value not already a PK will be generated</param>
        /// <param name="p_fname_get"></param>
        /// <param name="p_lname_get"></param>
        /// <param name="p_email_get"></param>
        /// <param name="p_phonenr_get"></param>
        /// <param name="p_image_get"></param>
        /// <returns>Create a person, if p_id_get is an empty string a random value not already a primary key will be assigned, if anything else is an empty string then a default value is assigned.</returns>
        public static p_person CreatePerson(string p_id_get, string p_fname_get, string p_lname_get, string p_email_get, string p_phonenr_get, string p_image_get)
        {
            //p_id_get is optional, use "" instead if you wish for random value.
            var checkperson = (from i in DBManager.db.p_person
                              where p_id_get == i.p_id
                              select i).ToList();
            if (checkperson.Count > 0)
                throw new ArgumentException("p_id is already used!");
            var p_id_set = "";
            if(p_id_get == "")
            {
                bool l = false;
                do
                {
                    var g = Guid.NewGuid().ToString();
                    var checkperson2 = (from i in DBManager.db.p_person
                                       where g == i.p_id
                                       select i).ToList();
                    if(checkperson2.Count <= 0)
                    {
                        l = true;
                        p_id_set = g;
                    }
                } while (!l);
            }
            else{
                p_id_set = p_id_get;
            }
            var p_fname_set = (p_fname_get == null || p_fname_get == "") ? p_fname_default : p_fname_get;
            var p_lname_set = (p_lname_get == null || p_lname_get == "") ? p_lname_default : p_lname_get;
            var p_email_set = (p_email_get == null || p_email_get == "") ? p_email_default : p_email_get;
            var p_phonenr_set = (p_phonenr_get == null || p_phonenr_get == "") ? p_phonenr_default : p_phonenr_get;
            var p_image_set = (p_image_get == null || p_image_get == "") ? p_image_default : p_image_get;
            p_person p = new p_person()
            {
                p_id = p_id_set,
                p_fname = p_fname_set,
                p_lname = p_lname_set,
                p_email = p_email_set,
                p_image = p_image_set,
                p_phonenr = p_phonenr_set
            };

            var r = DBManager.db.p_person.Add(p);
            DBManager.db.SaveChanges();
            return r;

        //    public string p_id { get; set; }
        //public string p_fname { get; set; }
        //public string p_lname { get; set; }
        //public string p_email { get; set; }
        //public string p_phonenr { get; set; }
        //public string p_image { get; set; }
    }

        public static IEnumerable<p_person> GetAllPersons()
        {
            var l = (from i in DBManager.db.p_person
                     select i).ToList();
            return l;
        }

        public static p_person DeletePerson(string p_id_get)
        {
            if (p_id_get == null || p_id_get == "")
                throw new ArgumentException("P_ID Null in DeleteProject");
            var l = (from p in DBManager.db.p_person
                     select p).ToList();
            var f = l.First();
            var i = l.RemoveAll(x => x.p_id == p_id_get);
            if (i == 0)
                throw new ArgumentException("No matching Persons in delete!");
            return f;
        }

        public static p_person UpdatePerson(string p_id_get, string p_fname_get, string p_lname_get, string p_email_get, string p_phonenr_get, string p_image_get)
        {
            var l = DeletePerson(p_id_get);
            var f = CreatePerson(p_id_get, p_fname_get, p_lname_get, p_email_get, p_phonenr_get, p_image_get);
            if (f == null)
            {
                DBManager.db.p_person.Add(l);
                DBManager.db.SaveChanges();
                throw new ArgumentException("Person could not be created in the update process!");
            }
            return f;
        }

    }
}