using SearchFight.Domain.Exceptions;
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
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException(nameof(name)) : name;
            results = new Dictionary<string, long>();
        }

        private readonly IDictionary<string, long> results;
        public string Name { get; private set; }
        public IReadOnlyDictionary<string, long> Results => results.ToImmutableDictionary();

        public void AddResult(string searchTerm, long numberOfResults)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException(nameof(searchTerm));
            }

            searchTerm = searchTerm.ToLower();
            if(results.ContainsKey(searchTerm))
            {
                throw new SearchTermAlreadyAddedException($"There is already a result record for {searchTerm}. If you want to replace that, use {nameof(ReplaceResult)} instead.");
            }

            results.Add(searchTerm, numberOfResults);
        }

        public void ReplaceResult(string searchTerm, long numberOfResults)
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
