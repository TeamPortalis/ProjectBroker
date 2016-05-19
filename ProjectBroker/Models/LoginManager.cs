using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models
{
    public interface ILoginManager
    {
        bool authenticate(IAuthToken authenticationToken);
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
}