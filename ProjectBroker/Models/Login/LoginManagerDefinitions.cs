using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models
{
    /// <summary>
    /// The Login manager.
    /// 
    /// A class implementing this interface has to take care of authenticating a user and later authorize said user.
    /// </summary>
    public interface ILoginManager
    {
        /// <summary>
        /// Authenticates the user if his/her login is successull
        /// </summary>
        /// <param name="authenticationToken">The token that contains the stanardizes login info.</param>
        /// <returns>A boolean which indicates if the login was successful or not.</returns>
        bool authenticate(IAuthToken authenticationToken);
        /// <summary>
        /// Shows if a user is authoized to do something.
        /// </summary>
        /// <param name="token">The standardizes information needed for authorization</param>
        /// <returns>A boolean indicating if the authorization was successful or not.</returns>
        bool authorize(IAuthorizeToken token);
    }

    /// <summary>
    /// The schema how data has to be stuctured, so that it can be used for authorization.
    /// </summary>
    public interface IAuthorizeToken
    {
        EntityRescritctionType EntityType {get;}
        string Username { get; }
    }

    /// <summary>
    /// The schema how data has to be structured, so that it can be used for login.
    /// </summary>
    public interface IAuthToken
    {
        AuthenticationType Type { get;  }
        String Token { get; }
        String Username { get; }
    }


    /// <summary>
    /// Allowed login types.
    /// 
    /// USER_PASS - Login happens with a username and password
    /// TOKEN - Login is tokenbased.
    /// </summary>
    public enum AuthenticationType
    {
        USER_PASS, TOKEN
    }
    
    /// <summary>
    /// Shows which kind of entity is authorized to use a certain resource
    /// </summary>
    public enum EntityRescritctionType: long
    {
        TEACHER = 1, STUDENT = 2, ADMIN = 4
    }

    /// <summary>
    /// The interface describing a AuthTokenFactory
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter data sturcture.</typeparam>
    public interface IAuthTokenFactory<TInput>
    {
        IAuthToken CreateAuthToken(TInput authParams);
    }

    /// <summary>
    /// The interface describing a AuthorizationTokenFactory
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter data sturcture.</typeparam>
    public interface IAutorizeTokenFactory<TInput>
    {
        IAuthorizeToken CreateAuthorizeToken(TInput authorizationParam);
    }
}