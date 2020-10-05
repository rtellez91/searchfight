using SearchFight.Core.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SearchFight.Tests.Core.Builders
{
    public class SearchFightResultsBuilderTest
    {
        [Fact]
        public void Should_SetSearchFightWinner_Given_Valid_Properties()
        {
            var searchFightWinner = "google";
            var searchFightResultsBuilder = new SearchFightResultsBuilder();
            searchFightResultsBuilder.SetSearchFightWinner(searchFightWinner);

            var searchFightResults = searchFightResultsBuilder.Build();

            Assert.Equal(searchFightWinner, searchFightResults.SearchFightWinner);
        }

        [Fact]
        public void Should_AddWinnersByProvider_Given_Valid_Properties()
        {
            var searchProvider1 = "google";
            var winner1 = ".net";
            var searchProvider2 = "bing";
            var winner2 = "java";

            var searchFightResultsBuilder = new SearchFightResultsBuilder();
            searchFightResultsBuilder.AddWinnerByProvider(searchProvider1, winner1);
            searchFightResultsBuilder.AddWinnerByProvider(searchProvider2, winner2);

            var searchFightResults = searchFightResultsBuilder.Build();

            Assert.Equal(2, searchFightResults.WinnerByProvider.Count);
            Assert.Equal(winner1, searchFightResults.WinnerByProvider[searchProvider1]);
            Assert.Equal(winner2, searchFightResults.WinnerByProvider[searchProvider2]);
        }

        [Fact]
        public void Should_AddResultBySearchTerm_Given_Valid_Properties()
        {
            var searchProvider1 = "google";
            var searchProvider2 = "bing";
            var searchTerm1 = ".net";
            var numberOfResults1_1 = 20;
            var numberOfResults1_2 = 15;
            var searchTerm2 = "java";
            var numberOfResults2_1 = 15;
            var numberOfResults2_2 = 20;

            var searchFightResultsBuilder = new SearchFightResultsBuilder();
            searchFightResultsBuilder.AddResultBySearchTerm(searchTerm1, searchProvider1, numberOfResults1_1);
            searchFightResultsBuilder.AddResultBySearchTerm(searchTerm1, searchProvider2, numberOfResults1_2);
            searchFightResultsBuilder.AddResultBySearchTerm(searchTerm2, searchProvider1, numberOfResults2_1);
            searchFightResultsBuilder.AddResultBySearchTerm(searchTerm2, searchProvider2, numberOfResults2_2);

            var searchFightResults = searchFightResultsBuilder.Build();

            Assert.Equal(2, searchFightResults.ResultsBySearchTerm.Count);

            var searchTerm1Results = searchFightResults.ResultsBySearchTerm[searchTerm1];
            Assert.Equal(2, searchTerm1Results.Count);
            Assert.Equal(numberOfResults1_1, searchTerm1Results[searchProvider1].NumberOfResults);
            Assert.Equal(numberOfResults1_2, searchTerm1Results[searchProvider2].NumberOfResults);

            var searchTerm2Results = searchFightResults.ResultsBySearchTerm[searchTerm2];
            Assert.Equal(2, searchTerm2Results.Count);
            Assert.Equal(numberOfResults2_1, searchTerm2Results[searchProvider1].NumberOfResults);
            Assert.Equal(numberOfResults2_2, searchTerm2Results[searchProvider2].NumberOfResults);
        }
    }
}
