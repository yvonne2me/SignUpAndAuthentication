using System;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Interfaces
{
    public interface IUserRepository
    {
        bool DoesUserExist(string email);
        AuthorizationResponse CreateNewUser(User user);
        void SaveNewUser(User user, AuthorizationResponse signUpResponse);
        User GetUser(Guid userId);
        User GetUser(string email);
        AuthorizationResponse CheckUserCredentials(SignInRequest signInRequest);
        AuthorizationResponse GetAuthorizationResponse(Guid userId);
    }
}
