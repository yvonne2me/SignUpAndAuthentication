using System;
using GenesisAutomation.Models;

namespace GenesisAutomation.Interfaces
{
    public interface ISignInService
    {
        AuthorizationResponse LogIn(SignInRequest signInRequest);
    }
}
