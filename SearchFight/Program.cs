using Microsoft.Extensions.DependencyInjection;
using SearchFight.ApplicationServices.Interfaces;
using SearchFight.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SearchFightConfiguration.SetupApplication();

            var fightSearchService = SearchFightConfiguration.ServiceProvider.GetService<ISearchFightService>();

            var fightSearchResults = await fightSearchService.RunFight(args);

            foreach (var resultBySearchTerm in fightSearchResults.ResultsBySearchTerm)
            {
                var sb = new StringBuilder();
                sb.Append($"{resultBySearchTerm.Key}: ");
                foreach (var searchProviderResult in resultBySearchTerm.Value)
                {
                    sb.Append($"{searchProviderResult.Key}: {searchProviderResult.Value.NumberOfResults} ");
                }
                Console.WriteLine(sb.ToString());
            }

            Console.WriteLine();

            foreach (var providerWinner in fightSearchResults.WinnerByProvider)
            {
                Console.WriteLine($"{providerWinner.Key} winner: {providerWinner.Value}");
            }

            Console.WriteLine($"\nTotal winner: {fightSearchResults.SearchFightWinner}");
            Console.ReadLine();
        }
    }
}
