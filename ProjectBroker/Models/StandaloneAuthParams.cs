namespace ProjectBroker.Models
{
    public class StandaloneAuthParams
    {

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string AuthToken { get; private set; }
        public AuthenticationType Type { get; private set; }

        private StandaloneAuthParams(string username, string authToken, bool isPasswordBased)
        {
            Username = username;
            if(isPasswordBased)
            {
                Password = authToken;
                AuthToken = null;
                Type = AuthenticationType.USER_PASS;
            }else
            {
                AuthToken = authToken;
                Password = null;
                Type = AuthenticationType.TOKEN;
            }
        }

        public static StandaloneAuthParams CreateByUserPass(string username, string Password)
        {
            return new StandaloneAuthParams(username, Password, true);
        }

        public static StandaloneAuthParams CreateByToken(string username, string token)
        {
            return new StandaloneAuthParams(username, token, false);
        }
    }
}