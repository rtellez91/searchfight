using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFight.Domain.Services
{
    public class SearchFightDomainService : ISearchFightDomainService
    {
        public string GetSearchTermWinner(IEnumerable<SearchFightProvider> searchFightProviders)
        {
            return searchFightProviders
                    .SelectMany(sfp => sfp.Results)
                    .GroupBy(kp => kp.Key)
                    .Select(g => new
                    {
                        SearchTerm = g.Key,
                        Total = g.Sum(st => st.Value)
                    })
                    .Aggregate((max, next) => max.Total > next.Total ? max: next)
                    .SearchTerm;
        }
    }
}
