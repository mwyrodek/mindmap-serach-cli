namespace mindmap_search.Tests
{
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Microsoft.Extensions.Logging;
    using mindmap_search.MapSearch;
    using mindmap_search.Model;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "SA1600", Justification = "Tests")]
    [SuppressMessage("ReSharper", "CA1707", Justification = "Tests name follow _ naming")]
    public class SearchMapsTest
    {
        private readonly IFixture fixture;

        public SearchMapsTest()
        {
            this.fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void LoadMapsData_AddsMap_NoIssues()
        {
            var logger = this.fixture.Create<Mock<ILogger<SearchMaps>>>();
            var searchMaps = new SearchMaps(logger.Object);
            var mapDatas = this.fixture.CreateMany<MapData>().ToList();

            searchMaps.LoadMapsData(mapDatas);
        }

        [Fact]
        public void SearchMap_NoResults_ReturnsEmptyList()
        {
            var logger = this.fixture.Create<Mock<ILogger<SearchMaps>>>();
            var searchMaps = new SearchMaps(logger.Object);
            var mapDatas = this.fixture.CreateMany<MapData>().ToList();
            searchMaps.LoadMapsData(mapDatas);
            const string searchedPhrase = "ThereIsNoSuchValue";

            var findNodesWithValue = searchMaps.FindNodesWithValue(searchedPhrase);

            Assert.Empty(findNodesWithValue);
        }

        [Fact]
        public void SearchMap_NoMapsLoaded_ThrowsError()
        {
            var logger = this.fixture.Create<Mock<ILogger<SearchMaps>>>();
            var searchMaps = new SearchMaps(logger.Object);

            const string searchedPhrase = "ThereIsNoSuchValue";

            Assert.Throws<DataException>(() => searchMaps.FindNodesWithValue(searchedPhrase));
        }

        [Fact]
        public void SearchMap_ResultFound_ReturnsCorectCount()
        {
            var logger = this.fixture.Create<Mock<ILogger<SearchMaps>>>();
            var searchMaps = new SearchMaps(logger.Object);
            var mapDatas = this.fixture.CreateMany<MapData>().ToList();
            searchMaps.LoadMapsData(mapDatas);
            const string searchedPhrase = "ThereIsSuchValue";
            mapDatas.First().Content.Add(searchedPhrase);
            mapDatas.Last().Content.Add(searchedPhrase);
            mapDatas[2].Content.Add(searchedPhrase);

            var findNodesWithValue = searchMaps.FindNodesWithValue(searchedPhrase);
            Assert.Equal(3, findNodesWithValue.Count);
        }

        [Fact]
        public void SearchMap_ResultFoundInLongerString_ReturnsWholePhrase()
        {
            var logger = this.fixture.Create<Mock<ILogger<SearchMaps>>>();
            var searchMaps = new SearchMaps(logger.Object);
            var mapDatas = this.fixture.CreateMany<MapData>().ToList();
            const string searchedPhrase = "ThereIsSuchValue";
            var paddedLeftPhrase = searchedPhrase.PadLeft(10, '%');
            mapDatas.First().Content.Add(paddedLeftPhrase);
            var paddedRightPhrase = searchedPhrase.PadRight(10, '@');
            mapDatas.Last().Content.Add(paddedRightPhrase);
            var fullyPaddedPhrase = searchedPhrase.PadLeft(5, '$').PadRight(2, '^');
            mapDatas[2].Content.Add(fullyPaddedPhrase);

            searchMaps.LoadMapsData(mapDatas);

            var findNodesWithValue = searchMaps.FindNodesWithValue(searchedPhrase);

            Assert.Equal(3, findNodesWithValue.Count);
            Assert.Equal(paddedLeftPhrase, findNodesWithValue[0].Result);
            Assert.Equal(paddedRightPhrase, findNodesWithValue[1].Result);
            Assert.Equal(fullyPaddedPhrase, findNodesWithValue[2].Result);
        }
    }
}