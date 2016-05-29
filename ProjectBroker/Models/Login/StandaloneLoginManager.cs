using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;
using System.Security.Cryptography;

namespace ProjectBroker.Models
{
    public class StandaloneLoginManage : ILoginManager, IAuthTokenFactory<StandaloneAuthParams>, IAutorizeTokenFactory<StandaloneAuthTokenParams>
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

        public bool authorize(IAuthorizeToken token)
        {

            var db = DBManager.db;

        }

        public IAuthToken CreateAuthToken(StandaloneAuthParams authParams)
        {
            if (authParams.Type == AuthenticationType.USER_PASS)
            {
                return new InternalAuthToken(authParams.Username, authParams.Password, AuthenticationType.USER_PASS);
            }
            else
            {
                return new InternalAuthToken(authParams.Username, authParams.AuthToken, AuthenticationType.TOKEN);
            }
        }

        public IAuthorizeToken CreateAuthorizeToken(StandaloneAuthTokenParams authorizationParam)
        {
            return new InternalAuthorizationToken(authorizationParam.Type, authorizationParam.Username);
        }



        /// <summary>
        /// Authorization Token for Login Manager
        /// </summary>

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


        /// <summary>
        /// Internal Representation for AuthorizationToken. 
        /// </summary>
        internal class InternalAuthorizationToken : IAuthorizeToken
        {
            private EntityRescritctionType entityType;
            private string username;

            public InternalAuthorizationToken(EntityRescritctionType _entityType, string _username)
            {
                entityType = _entityType;
                username = _username;
            }


            public EntityRescritctionType EntityType
            {
                get
                {
                    return entityType;   
                }
            }

            public string Username
            {
                get
                {
                    return username;
                }
            }
        }
    }


    /// <summary>
    /// AuthorizationToken parameters.
    /// </summary>
    public class StandaloneAuthTokenParams
    {

        public string Username { get; set; }
        public EntityRescritctionType Type { get; set; }

    }
}