using System;
using GenesisAutomation.Models;

namespace GenesisAutomation.Interfaces
{
    public interface ITokenService
    {
        string CreateToken();
        bool ValidateTokens(string savedUserToken, string currentUserToken);
    }
}
