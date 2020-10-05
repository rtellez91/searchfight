using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Infrastructure.SearchProviders
{
    public class SearchProvidersConfiguration
    {
        public Dictionary<string, SearchProviderConfiguration> SearchProvidersConfigurations { get; set; }
    }

    public class SearchProviderConfiguration
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}
