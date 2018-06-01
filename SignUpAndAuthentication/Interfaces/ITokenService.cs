using System;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Interfaces
{
    public interface ITokenService
    {
        string CreateToken();
        bool ValidateTokens(string savedUserToken, string currentUserToken);
    }
}
