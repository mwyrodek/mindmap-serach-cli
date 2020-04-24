using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Logging;
using mindmap_search.MapSearch;
using mindmap_search.Model;
using Moq;
using Xunit;

namespace mindmap_search.tests
{
    public class SearchMapsTest
    {
        [Fact]
        public void LoadMapsData_AddsMap_NoIssues()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var logger = fixture.Create<Mock<ILogger<SearchMaps>>>();
            var searchMaps = new SearchMaps(logger.Object);
            var mapDatas = fixture.CreateMany<MapData>().ToList();
            
            searchMaps.LoadMapsData(mapDatas);
        }
    }
}