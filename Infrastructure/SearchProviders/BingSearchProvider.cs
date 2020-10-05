using Microsoft.Extensions.Options;
using SearchFight.Core.Model;
using SearchFight.Infrastructure.SearchProviders.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchFight.Infrastructure.SearchProviders
{
    public class BingSearchProvider : ISearchProvider
    {
        private readonly HttpClient httpClient;
        private readonly SearchProviderConfiguration searchProvidersConfiguration;
        public string Name => "Bing";

        public BingSearchProvider(HttpClient httpClient, IOptions<SearchProvidersConfiguration> searchProvidersConfiguration)
        {
            this.httpClient = httpClient;
            this.searchProvidersConfiguration = searchProvidersConfiguration.Value.SearchProvidersConfigurations[Name];
        }

        public async Task<SearchResult> Search(SearchRequest searchRequest)
        {
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", searchProvidersConfiguration.ApiKey);
            var requestUrl = new Uri($"{searchProvidersConfiguration.Url}?q={searchRequest.SearchTerm}");
            using (var response = await httpClient.GetAsync(requestUrl))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong when getting results from search provider.");
                }
                var stringResponse = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var deserializedResult = JsonSerializer.Deserialize<BingResponse>(stringResponse, options);

                return new SearchResult
                {
                    NumberOfResults = deserializedResult.WebPages.TotalEstimatedMatches
                };
            }
        }
    }
}
