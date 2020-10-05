using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Infrastructure.SearchProviders.Models
{
    public class BingResponse
    {
        public WebAnswer WebPages { get; set; }
    }

    public class WebAnswer
    {
        public long TotalEstimatedMatches { get; set; }
    }
}
