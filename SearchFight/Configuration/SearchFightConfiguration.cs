using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.ApplicationServices.Interfaces;
using SearchFight.ApplicationServices.Services;
using SearchFight.Core.Builders;
using SearchFight.Domain.Services;
using SearchFight.Infrastructure.SearchProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace SearchFight.Configuration
{
    public static class SearchFightConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        public static void SetupApplication()
        {
            BuildConfigurationRoot();
            var serviceCollection = BuildServiceCollection();
            ServiceProvider = serviceCollection.BuildServiceProvider();
            
        }
        private static void BuildConfigurationRoot()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
        }
        private static IServiceCollection BuildServiceCollection()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.Configure<SearchProvidersConfiguration>(Configuration);

            serviceCollection.AddTransient<ISearchProvider, GoogleSearchProvider>();
            serviceCollection.AddTransient<ISearchProvider, BingSearchProvider>();
            serviceCollection.AddSingleton<ISearchFightResultsBuilder, SearchFightResultsBuilder>();

            serviceCollection.AddSingleton<ISearchFightDomainService, SearchFightDomainService>();

            serviceCollection.AddTransient<ISearchFightService, SearchFightService>();

            serviceCollection.AddTransient<HttpClient>();

            return serviceCollection;
        }
    }
}
