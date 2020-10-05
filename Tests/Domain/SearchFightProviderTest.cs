using SearchFight.Domain;
using SearchFight.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SearchFight.Tests.Domain
{
    public class SearchFightProviderTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Should_Throw_ArgumentException_When_Creating_With_InvalidParameters(string searchProvider)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var searchFightProvider = new SearchFightProvider(searchProvider);
            });
        }

        [Fact]
        public void Should_CreateSearchFightProvider_Instance_With_Valid_Parameters()
        {
            var providerName = "Google";
            var searchFightProvider = new SearchFightProvider(providerName);

            Assert.NotNull(searchFightProvider);
            Assert.Equal(providerName, searchFightProvider.Name);
            Assert.Empty(searchFightProvider.Results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Should_Throw_ArgumentException_When_AddingResult_With_Invalid_SearchTerm(string searchTerm)
        {
            var providerName = "Google";
            var searchFightProvider = new SearchFightProvider(providerName);

            Assert.Throws<ArgumentException>(() =>
            {
                searchFightProvider.AddResult(searchTerm, 0);
            });
        }

        [Fact]
        public void Should_Throw_SearchTermAlreadyAddedException_When_AddingResult_With_Repeated_SearchTerm()
        {
            var providerName = "Google";
            var searchFightProvider = new SearchFightProvider(providerName);

            var searchTerm = ".net";
            searchFightProvider.AddResult(searchTerm, 10);
            Assert.Throws<SearchTermAlreadyAddedException>(() =>
            {
                searchFightProvider.AddResult(searchTerm, 10);
            });
        }

        [Fact]
        public void Should_AddResult_With_Different_SearchTerms()
        {
            var providerName = "Google";
            var searchFightProvider = new SearchFightProvider(providerName);

            var searchTerm = ".net";
            var numberOfResults = 10;
            searchFightProvider.AddResult(searchTerm, numberOfResults);

            Assert.True(searchFightProvider.Results.ContainsKey(searchTerm));
            Assert.Equal(numberOfResults, searchFightProvider.Results[searchTerm]);
        }

        [Fact]
        public void Should_ReplaceResult_With_Same_SearchTerms()
        {
            var providerName = "Google";
            var searchFightProvider = new SearchFightProvider(providerName);

            var searchTerm = ".net";
            var numberOfResults = 10;
            searchFightProvider.AddResult(searchTerm, numberOfResults);

            numberOfResults = 20;
            searchFightProvider.ReplaceResult(searchTerm, numberOfResults);

            Assert.True(searchFightProvider.Results.ContainsKey(searchTerm));
            Assert.Equal(numberOfResults, searchFightProvider.Results[searchTerm]);
        }

        [Fact]
        public void Should_GetWinner()
        {
            var providerName = "Google";
            var searchFightProvider = new SearchFightProvider(providerName);

            var searchTerm1 = ".net";
            var numberOfResults1 = 50;
            searchFightProvider.AddResult(searchTerm1, numberOfResults1);

            var searchTerm2 = "java";
            var numberOfResults2 = 40;
            searchFightProvider.ReplaceResult(searchTerm2, numberOfResults2);

            var searchTerm3 = "ruby";
            var numberOfResults3 = 30;
            searchFightProvider.ReplaceResult(searchTerm3, numberOfResults3);

            var winner = searchFightProvider.GetWinner();

            Assert.Equal(searchTerm1, winner);
        }
    }
}
