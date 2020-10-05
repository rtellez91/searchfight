using SearchFight.Core.Model;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SearchFight.Core.Builders
{
    public class SearchFightResultsBuilder : ISearchFightResultsBuilder
    {
        Dictionary<string, string> winnersByProvider = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, SearchResult>> resultsBySearchTerm = new Dictionary<string, Dictionary<string, SearchResult>>();
        string searchFightWinner = null;
        public void AddResultBySearchTerm(string searchTerm, string searchProvider, long numberOfResults)
        {
            if (!resultsBySearchTerm.TryGetValue(searchTerm, out var value))
            {
                value = new Dictionary<string, SearchResult>();
                resultsBySearchTerm.Add(searchTerm, value);
            }
            value.Remove(searchProvider);
            value.Add(searchProvider, new SearchResult { NumberOfResults = numberOfResults });
        }

        public void AddWinnerByProvider(string searchProvider, string searchTerm)
        {
            winnersByProvider.Remove(searchProvider);
            winnersByProvider.Add(searchProvider, searchTerm);
        }

        public void SetSearchFightWinner(string searchFightWinner)
        {
            this.searchFightWinner = searchFightWinner;
        }

        public SearchFightResults Build()
        {
            return new SearchFightResults
            {
                ResultsBySearchTerm = resultsBySearchTerm.Select(kv => new KeyValuePair<string, IReadOnlyDictionary<string, SearchResult>>(kv.Key, kv.Value)).ToImmutableDictionary(),
                SearchFightWinner = searchFightWinner,
                WinnerByProvider = winnersByProvider
            };
        }
    }
}
