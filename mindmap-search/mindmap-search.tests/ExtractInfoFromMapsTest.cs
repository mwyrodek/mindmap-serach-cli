namespace mindmap_search.Tests
{        using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Microsoft.Extensions.Logging;
    using mindmap_search.FileTypes;
    using mindmap_search.FolderCrawl;
    using mindmap_search.Model;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "SA1600", Justification = "Tests")]
    [SuppressMessage("ReSharper", "CA1707", Justification = "Tests name follow _ naming")]
    public class ExtractInfoFromMapsTest
    {
        private readonly IFixture fixture;

        public ExtractInfoFromMapsTest()
        {
            this.fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void ExtracInfo_DelegatesJobImodel()
        {
            var logger = this.fixture.Create<Mock<ILogger<ExtractInfoFromMaps>>>();
            var mindMapMock = this.fixture.Create <Mock<IMindMapType>>();
            var fakeData = new List<MapData>();
            mindMapMock.Setup(x => x.ExtractData(It.IsAny<FileInfo[]>())).Returns(fakeData);
            var sut = new ExtractInfoFromMaps(logger.Object, mindMapMock.Object);

            var actualData = sut.ExtartInfo(new FileInfo[2]);
            Assert.Equal(fakeData, actualData);
        }
    }
}