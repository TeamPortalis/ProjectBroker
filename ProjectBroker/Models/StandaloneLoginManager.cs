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
            //TO-DO ASP.NET login management
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