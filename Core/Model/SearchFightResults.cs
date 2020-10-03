using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Core.Model
{
    public class SearchFightResults
    {
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, SearchResult>> ResultsBySearchTerm { get; set; }
        public IReadOnlyDictionary<string, string> WinnerByProvider { get; set; }
        public string SearchFightWinner { get; set; }
    }
}
