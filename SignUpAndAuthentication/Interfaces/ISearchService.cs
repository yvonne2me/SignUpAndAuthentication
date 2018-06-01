using System;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Interfaces
{
    public interface ISearchService
    {
        User GetUser(Guid userId, string token);
    }
}
