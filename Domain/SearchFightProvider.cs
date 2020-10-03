using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SearchFight.Domain
{
    public class SearchFightProvider
    {
        public SearchFightProvider(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            results = new Dictionary<string, int>();
        }

        private readonly IDictionary<string, int> results;
        public string Name { get; private set; }
        public IReadOnlyDictionary<string, int> Results => results.ToImmutableDictionary();

        public void AddResult(string searchTerm, int numberOfResults)
        {
            searchTerm = searchTerm.ToLower();
            if(results.ContainsKey(searchTerm))
            {
                throw new Exception($"There is already a result record for {searchTerm}. If you want to replace that, use {nameof(ReplaceResult)} instead.");
            }

            results.Add(searchTerm, numberOfResults);
        }

        public void ReplaceResult(string searchTerm, int numberOfResults)
        {
            searchTerm = searchTerm.ToLower();
            results.Remove(searchTerm);
            AddResult(searchTerm, numberOfResults);
        }

        public string GetWinner()
        {
            return results.Aggregate((maximum, next) => maximum.Value > next.Value ? maximum : next).Key;
        }
    }
}
