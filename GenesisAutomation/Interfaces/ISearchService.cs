using System;
using GenesisAutomation.Models;

namespace GenesisAutomation.Interfaces
{
    public interface ISearchService
    {
        User GetUser(Guid userId, string token);
    }
}
