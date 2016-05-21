using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;
using System.Security.Cryptography;

namespace ProjectBroker.Models
{
    public class StandaloneLoginManage : ILoginManager, IAuthTokenFactory<StandaloneAuthParams>
    {

        private static projectbrokerEntities db = new projectbrokerEntities();

        public bool authenticate(IAuthToken authenticationToken)
        {
            if(authenticationToken.Type == AuthenticationType.USER_PASS)
            {
                lpr_login_person_relation login = (from p in db.lpr_login_person_relation
                                                   where p.lpr_username == authenticationToken.Username
                                                   select p).FirstOrDefault();
                if (login == null)
                    return false;

                SHA512 hasher = SHA512.Create();
                var hashString = System.Text.Encoding.UTF8.GetBytes((authenticationToken.Token + login.l_login_info.l_salt).ToCharArray());
            
                var hashed = hasher.ComputeHash(hashString);
                if (Convert.ToBase64String(hashed).Equals(login.l_login_info.l_authtoken))
                    return true;
                return false;
                
                
            }
        }

        public IAuthToken CreateAuthToken(StandaloneAuthParams authParams)
        {
            
        }
    }
}