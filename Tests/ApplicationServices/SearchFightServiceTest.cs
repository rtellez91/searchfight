﻿using Moq;
using SearchFight.ApplicationServices.Services;
using SearchFight.Core.Builders;
using SearchFight.Core.Model;
using SearchFight.Domain;
using SearchFight.Domain.Services;
using SearchFight.Infrastructure.SearchProviders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SearchFight.Tests.ApplicationServices
{
    public class SearchFightServiceTest
    {
        Mock<ISearchProvider> googleProvider;
        Mock<ISearchProvider> bingProvider;
        Mock<ISearchFightDomainService> searchFightDomainService;
        Mock<ISearchFightResultsBuilder> searchFightResultsBuilder;
        SearchFightService searchFightService;

        public SearchFightServiceTest()
        {
            googleProvider = new Mock<ISearchProvider>();
            googleProvider.Setup(gp => gp.Name).Returns("Google");

            bingProvider = new Mock<ISearchProvider>();
            bingProvider.Setup(bp => bp.Name).Returns("Bing");

            searchFightDomainService = new Mock<ISearchFightDomainService>();
            
            searchFightResultsBuilder = new Mock<ISearchFightResultsBuilder>();            

            searchFightService = new SearchFightService(new[] { googleProvider.Object, bingProvider.Object }, searchFightDomainService.Object, searchFightResultsBuilder.Object);
        }

        [Fact]
        public void Should_Search_Once_PerUniqueWord()
        {
            googleProvider.Setup(gp => gp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());
            bingProvider.Setup(bp => bp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());

            var searchTerms = new[] { ".net", "java", "Java" };

            searchFightService.RunFight(searchTerms);

            googleProvider.Verify(gp => gp.Search(It.IsAny<SearchRequest>()), Times.Exactly(2));
            bingProvider.Verify(gp => gp.Search(It.IsAny<SearchRequest>()), Times.Exactly(2));
        }

        [Fact]
        public void Should_AddResultBySearchTerm_Once_PerUniqueWord()
        {
            googleProvider.Setup(gp => gp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());
            bingProvider.Setup(bp => bp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());

            var searchTerms = new[] { ".net", "java", "Java" };

            searchFightService.RunFight(searchTerms);

            searchFightResultsBuilder.Verify(builder => builder.AddResultBySearchTerm(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(4));
        }

        [Fact]
        public void Should_AddWinnerByProvider_Once_PerUniqueWord()
        {
            googleProvider.Setup(gp => gp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());
            bingProvider.Setup(bp => bp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());

            var searchTerms = new[] { ".net", "java", "Java" };

            searchFightService.RunFight(searchTerms);

            searchFightResultsBuilder.Verify(builder => builder.AddWinnerByProvider(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public void Should_SetFightWinnerByProvider_Once()
        {
            googleProvider.Setup(gp => gp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());
            bingProvider.Setup(bp => bp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());

            var searchTerms = new[] { ".net", "java", "Java" };

            searchFightService.RunFight(searchTerms);

            searchFightResultsBuilder.Verify(builder => builder.SetSearchFightWinner(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Should_GetSearchFightWinner_Once()
        {
            googleProvider.Setup(gp => gp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());
            bingProvider.Setup(bp => bp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());

            var searchTerms = new[] { ".net", "java", "Java" };

            searchFightService.RunFight(searchTerms);

            searchFightDomainService.Verify(service => service.GetSearchFightWinner(It.IsAny<IEnumerable<SearchFightProvider>>()), Times.Once);
        }

        [Fact]
        public void Should_Build_SearchFightResults_Once()
        {
            googleProvider.Setup(gp => gp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());
            bingProvider.Setup(bp => bp.Search(It.IsAny<SearchRequest>())).Returns(new SearchResult());

            var searchTerms = new[] { ".net", "java", "Java" };

            searchFightService.RunFight(searchTerms);

            searchFightResultsBuilder.Verify(builder => builder.Build(), Times.Once);
        }
    }
}