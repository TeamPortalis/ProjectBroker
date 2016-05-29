using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models
{
    public interface ILoginManager
    {
        bool authenticate(IAuthToken authenticationToken);
        bool authorize(IAuthorizeToken token);
    }

    public interface IAuthorizeToken
    {
        EntityRescritctionType EntityType {get;}
        string Username { get; }
    }

    public interface IAuthToken
    {
        AuthenticationType Type { get;  }
        String Token { get; }
        String Username { get; }
    }

    public enum AuthenticationType
    {
        USER_PASS, TOKEN
    }
    
    public enum EntityRescritctionType: long
    {
        TEACHER = 1, STUDENT = 2, ADMIN = 4
    }

    public interface IAuthTokenFactory<TInput>
    {
        IAuthToken CreateAuthToken(TInput authParams);
    }

    public interface IAutorizeTokenFactory<TInput>
    {
        IAuthorizeToken CreateAuthorizeToken(TInput authorizationParam);
    }
}