using SearchFight.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Infrastructure.SearchProviders
{
    public interface ISearchProvider
    {
        string Name { get; }
        Task<SearchResult> Search(SearchRequest searchRequest);
    }
}
