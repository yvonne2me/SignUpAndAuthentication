using System;
using System.Threading.Tasks;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Interfaces
{
    public interface ISignUpService
    {
        AuthorizationResponse CreateNewUser(User user);
    }
}
