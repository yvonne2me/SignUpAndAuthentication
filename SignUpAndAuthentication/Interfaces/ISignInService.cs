using System;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Interfaces
{
    public interface ISignInService
    {
        AuthorizationResponse LogIn(SignInRequest signInRequest);
    }
}
