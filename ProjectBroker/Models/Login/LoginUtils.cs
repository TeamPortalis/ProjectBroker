using ProjectBroker.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBroker.Models.Login
{
    public class LoginUtils
    {
        public static p_person UserForLogin(string username)
        {
            var result = (from d in DBManager.db.lpr_login_person_relation
                          where d.lpr_username == username
                          select d.p_person).ToList().FirstOrDefault();

            return result;
        }
    }
}
