using System;
using System.Threading.Tasks;
using GenesisAutomation.Models;

namespace GenesisAutomation.Interfaces
{
    public interface ISignUpService
    {
        AuthorizationResponse CreateNewUser(User user);
    }
}
