using SearchFight.ApplicationServices.Interfaces;
using SearchFight.Core.Builders;
using SearchFight.Core.Model;
using SearchFight.Domain;
using SearchFight.Domain.Services;
using SearchFight.Infrastructure.SearchProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.ApplicationServices.Services
{
    public class SearchFightService : ISearchFightService
    {
        private readonly IEnumerable<ISearchProvider> searchProviders;
        private readonly ISearchFightDomainService searchFightDomainService;
        private readonly ISearchFightResultsBuilder searchFightResultsBuilder;

        public SearchFightService(IEnumerable<ISearchProvider> searchProviders, ISearchFightDomainService searchFightDomainService, ISearchFightResultsBuilder searchFightResultsBuilder)
        {
            this.searchProviders = searchProviders;
            this.searchFightDomainService = searchFightDomainService;
            this.searchFightResultsBuilder = searchFightResultsBuilder;
        }

        public async Task<SearchFightResults> RunFight(IEnumerable<string> searchTerms)
        {
            searchTerms = searchTerms.Select(st => st.ToLower()).Distinct();
            var searchFightProviders = new List<SearchFightProvider>();
            
            foreach (var searchProvider in searchProviders)
            {
                var searchFightProvider = new SearchFightProvider(searchProvider.Name);

                foreach (var searchTerm in searchTerms)
                {
                    var searchResult = await searchProvider.Search(new SearchRequest { SearchTerm = searchTerm });
                    searchFightProvider.AddResult(searchTerm, searchResult.NumberOfResults);
                    searchFightResultsBuilder.AddResultBySearchTerm(searchTerm, searchProvider.Name, searchResult.NumberOfResults);
                }

                searchFightResultsBuilder.AddWinnerByProvider(searchFightProvider.Name, searchFightProvider.GetWinner());
                searchFightProviders.Add(searchFightProvider);
            }

            var searchTermWinner = searchFightDomainService.GetSearchFightWinner(searchFightProviders);
            searchFightResultsBuilder.SetSearchFightWinner(searchTermWinner);

            return searchFightResultsBuilder.Build();
        }
    }
}
