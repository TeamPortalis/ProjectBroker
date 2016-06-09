using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models
{
    /// <summary>
    /// A test manager for login data.
    /// </summary>
    public class MockupLoginManager : ILoginManager
    {

        public static IAuthToken CreateLoginToken(string username, string pass)
        {
            return new InternalAuthToken(username, pass);
        }

        public class InternalAuthToken : IAuthToken
        {
            private string user;
            private string pass;

            internal InternalAuthToken(String username, String password)
            {
                user = username;
                pass = password;
            } 

            public string Token
            {
                get
                {
                    return pass;
                }
            }

            public AuthenticationType Type
            {
                get
                {
                    return AuthenticationType.USER_PASS;
                }
            }

            public string Username
            {
                get
                {
                    return user;
                }
            }
        }

        private Dictionary<String, String> loginDB = new Dictionary<string, string>();

        public MockupLoginManager()
        {
            loginDB.Add("test", "1234");
            loginDB.Add("mrtest", "IAmSoGreat");
        }

        public bool authenticate(IAuthToken authenticationToken)
        {
            if (!loginDB.ContainsKey(authenticationToken.Username))
                return false;
            return loginDB[authenticationToken.Username].Equals(authenticationToken.Token);
        }

        public bool authorize(IAuthorizeToken token)
        {
            throw new NotImplementedException();
        }
    }
}