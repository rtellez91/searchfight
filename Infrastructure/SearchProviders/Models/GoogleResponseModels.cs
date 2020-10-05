using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Infrastructure.SearchProviders.Models
{
    public class GoogleResponse
    {
        public SearchInformation SearchInformation { get; set; }
    }

    public class SearchInformation
    {
        public string TotalResults { get; set; }
    }
}
