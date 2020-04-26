using System.Collections.Generic;
using System.IO;
using mindmap_search.FileTypes;
using mindmap_search.FolderCrawl;
using mindmap_search.Model;

namespace mindmap_search.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Microsoft.Extensions.Logging;
    using mindmap_search.Engine;
    using mindmap_search.MapSearch;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "SA1600", Justification = "Tests")]
    [SuppressMessage("ReSharper", "CA1707", Justification = "Tests name follow _ naming")]
    public class EngineTests
    {
        private readonly IFixture fixture;
        private readonly Mock<ILogger<Engine>> loggerMock;
        private readonly Mock<ISearchFiles> searchFilesMock;
        private readonly Mock<ISearchMaps> searchMapMock;
        private readonly Mock<IExtractInfoFromMaps> extractMock;
        private IEngine sut;

        public EngineTests()
        {
            this.fixture = new Fixture().Customize(new AutoMoqCustomization());
            this.loggerMock = this.fixture.Create<Mock<ILogger<Engine>>>();
            this.searchFilesMock = this.fixture.Create<Mock<ISearchFiles>>();
            this.searchMapMock = this.fixture.Create<Mock<ISearchMaps>>();
            this.extractMock = this.fixture.Create<Mock<IExtractInfoFromMaps>>();
        }

        [Fact]
        public void FindMaps_Path_IsPassedToFindAllMaps()
        {
            string path = "lets pretend this is a path";

            this.sut = new Engine(this.loggerMock.Object, this.searchMapMock.Object, this.extractMock.Object, this.searchFilesMock.Object);

            this.sut.FindMaps(path);

            this.searchFilesMock.Verify(f => f.FindAllMaps(path), Times.Once);
        }

        [Fact]
        public void FindMaps_Path_DataIsExtracted()
        {
            string path = "lets pretend this is a path";
            var expectedData = new FileInfo[2];
            this.searchFilesMock
                .Setup(p => p.FindAllMaps(path))
                .Returns(expectedData);
            this.sut = new Engine(this.loggerMock.Object, this.searchMapMock.Object, this.extractMock.Object, this.searchFilesMock.Object);

            this.sut.FindMaps(path);

            this.extractMock.Verify(f => f.ExtartInfo(expectedData), Times.Once);
        }

        [Fact]
        public void FindMaps_Path_DataIsLoaded()
        {
            string path = "lets pretend this is a path";
            var mapsdata = this.fixture.Create<List<MapData>>();

            this.extractMock
                .Setup(p => p.ExtartInfo(It.IsAny<FileInfo[]>()))
                .Returns(mapsdata);
            this.sut = new Engine(this.loggerMock.Object, this.searchMapMock.Object, this.extractMock.Object, this.searchFilesMock.Object);

            this.sut.FindMaps(path);

            this.searchMapMock.Verify(f => f.LoadMapsData(mapsdata), Times.Once);
        }

        [Fact]
        public void Search_query_ReturnsSearchResultList()
        {
            var expectedResult = this.fixture.Create<List<SearchResult>>();
            string query = "i am looking for this";
            this.searchMapMock.Setup(s => s.FindNodesWithValue(query)).Returns(expectedResult);

            this.sut = new Engine(this.loggerMock.Object, this.searchMapMock.Object, this.extractMock.Object, this.searchFilesMock.Object);

            var searchResults = this.sut.Search(query);

            Assert.Equal(expectedResult, searchResults);
        }
    }
}