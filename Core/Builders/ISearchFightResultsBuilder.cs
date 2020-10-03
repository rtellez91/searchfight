using SearchFight.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Core.Builders
{
    public interface ISearchFightResultsBuilder
    {
        void SetSearchTermWinner(string searchTermWinner);
        void AddResultBySearchTerm(string searchTerm, string searchProvider, int numberOfResults);
        void AddWinnerByProvider(string searchProvider, string searchFightWinner);
        SearchFightResults Build();
    }
}
