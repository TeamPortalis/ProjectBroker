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
            var connectionString = DBManager.db.Database.Connection.ConnectionString;

            if (authenticationToken.Type == AuthenticationType.TOKEN)
                return false;

            using (var conn = new Npgsql.NpgsqlConnection(connectionString))
            {
                conn.Open();
                using(var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM projectbrokerschema.uf_authenticate(@user, @pass) AS auth;";

                    var user = command.CreateParameter();
                    user.ParameterName = "@user";
                    user.Value = authenticationToken.Username;

                    var pass = command.CreateParameter();
                    pass.ParameterName = "@pass";
                    pass.Value = authenticationToken.Token;

                    command.Parameters.Add(user);
                    command.Parameters.Add(pass);

                    var reader = command.ExecuteReader();
                    reader.Read();
                    string pid = (string)reader.GetValue(0);
                    bool auth = (bool)reader.GetValue(1);

                    return auth;

                }
            }

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