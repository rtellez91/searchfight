using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.Infrastructure.SearchProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Infrastructure
{
    public static class InfrastructureDependenciesConfigurations
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddOptions<SearchProviderConfiguration>();
            services.AddTransient<ISearchProvider, BingSearchProvider>();
        }
    }
}
