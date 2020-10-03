using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Domain.Services
{
    public interface ISearchFightDomainService
    {
        string GetSearchTermWinner(IEnumerable<SearchFightProvider> searchFightProviders);
    }
}
