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


        public bool authenticate(IAuthToken authenticationToken)
        {
            if(authenticationToken.Type == AuthenticationType.USER_PASS)
            {
                lpr_login_person_relation login = (from p in DBManager.db.lpr_login_person_relation
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
            return false;
        }

        public IAuthToken CreateAuthToken(StandaloneAuthParams authParams)
        {
             if(authParams.Type == AuthenticationType.USER_PASS)
            {
                return new InternalAuthToken(authParams.Username, authParams.Password, AuthenticationType.USER_PASS);
            } else
            {
                return new InternalAuthToken(authParams.Username, authParams.AuthToken, AuthenticationType.TOKEN);
            }
        }

        private class InternalAuthToken : IAuthToken
        {

            public InternalAuthToken(string username, string token, AuthenticationType type)
            {
                Token = token;
                Type = type;
                Username = username;
            }

            public string Token
            {
                get; private set;
               
            }

            public AuthenticationType Type
            {
                get; private set;
            }

            public string Username
            {
                get; private set;
            }
        }
    }
}