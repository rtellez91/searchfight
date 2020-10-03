using SearchFight.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Infrastructure.SearchProviders
{
    public interface ISearchProvider
    {
        string Name { get; }
        SearchResult Search(SearchRequest searchRequest);
    }
}
