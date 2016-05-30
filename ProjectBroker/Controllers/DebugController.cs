using ProjectBroker.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ProjectBroker.Controllers
{
    public class DebugController : Controller
    {
        // GET: Debug
        public ActionResult Index()
        {
            ViewBag.NoTestData = false;
            return View();
        }

        public ActionResult TestData()
        {
            var db = DBManager.db;

            p_person p = new p_person()
            {
                p_id = "ABC1231230",
                p_fname = "Tester",
                p_lname = "McTest",
                p_email = "test@test.de"
            };

            db.p_person.Add(p);
            db.SaveChanges();

            d_department d = new d_department()
            {
                d_id = "3333333331",
                d_name = "Test Department 1."
            };
            db.d_department.Add(d);
            db.SaveChanges();

            s_student s = new s_student()
            {
                s_nr = p.p_id,
                s_address = "New York City",
                s_dob = new DateTime(1998, 3, 12),
                s_phonenr = "+43 TEST",
                s_d_department = d.d_id
            };
            db.s_student.Add(s);
            db.SaveChanges();

            var b = db.Database.Connection.ConnectionString;

            using (var conn = new Npgsql.NpgsqlConnection(connectionString: b))
            {
                conn.Open();
               
                using (Npgsql.NpgsqlCommand q = conn.CreateCommand())
                {
                    q.CommandText = "SELECT projectbrokerschema.uf_create_new_user(@user, @pass, @pid)";
                    Npgsql.NpgsqlParameter user =  q.CreateParameter();
                    user.ParameterName = "@user";
                    user.Value = "test";

                    Npgsql.NpgsqlParameter pass = q.CreateParameter();
                    pass.ParameterName = "@pass";
                    pass.Value = "1234";

                    Npgsql.NpgsqlParameter lid = q.CreateParameter();
                    lid.ParameterName = "@pid";
                    lid.Value = s.s_nr;

                  

                    q.Parameters.Add(user);
                    q.Parameters.Add(pass);
                    q.Parameters.Add(lid);
                    q.ExecuteNonQuery();

                }
            }


            ViewBag.NoTestData = true;
            return View("Index");
        }
    }
}