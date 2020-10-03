using SearchFight.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.ApplicationServices.Interfaces
{
    public interface ISearchFightService
    {
        SearchFightResults RunFight(IEnumerable<string> searchTerms);
    }
}
