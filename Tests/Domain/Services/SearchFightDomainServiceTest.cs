using SearchFight.Domain;
using SearchFight.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace SearchFight.Tests.Domain.Services
{
    public class SearchFightDomainServiceTest
    {
        [Fact]
        public void Should_Throw_ArgumentException_When_Invoking_With_NullParameters()
        {
            var searchFightDomainService = new SearchFightDomainService();
            Assert.Throws<ArgumentException>(() => { searchFightDomainService.GetSearchFightWinner(null); });
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_Invoking_With_EmptyCollection()
        {
            var searchFightDomainService = new SearchFightDomainService();
            Assert.Throws<ArgumentException>(() => { searchFightDomainService.GetSearchFightWinner(new List<SearchFightProvider>()); });
        }

        [Fact]
        public void Should_Return_SearchFight_Winner()
        {
            var googleProvider = new SearchFightProvider("Google");
            var bingProvider = new SearchFightProvider("Bing");
            var searchTerm1 = ".net";
            googleProvider.AddResult(searchTerm1, 30);
            bingProvider.AddResult(searchTerm1, 20);

            var searchTerm2 = "java";
            googleProvider.AddResult(searchTerm2, 10);
            bingProvider.AddResult(searchTerm2, 10);

            var searchTerm3 = "ruby";
            googleProvider.AddResult(searchTerm3, 15);
            bingProvider.AddResult(searchTerm3, 25);

            var searchFightProviders = new List<SearchFightProvider>();
            searchFightProviders.Add(googleProvider);
            searchFightProviders.Add(bingProvider);

            var searchFightDomainService = new SearchFightDomainService();
            var searchFightWinner = searchFightDomainService.GetSearchFightWinner(searchFightProviders);

            Assert.Equal(searchTerm1, searchFightWinner);
        }
    }
}
