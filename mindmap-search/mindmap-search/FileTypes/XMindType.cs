namespace mindmap_search.FileTypes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using mindmap_search.Model;

    /// <summary>
    /// Implementation of IMindMapTypes for maps of Xmind
    /// </summary>
    public class XMindType : IMindMapType
    {
        private const string InsideFileJson = "content.json";
        private const string InsideFileXml = "content.xml";

        private readonly ILogger logger;
        private readonly IExctrectFromArchive archiveExctractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMindType"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="archiveExctractor">archive extractor.</param>
        public XMindType(ILogger<XMindType> logger, IExctrectFromArchive archiveExctractor)
        {
            this.logger = logger;
            this.archiveExctractor = archiveExctractor;
        }

        /// <inheritdoc/>
        public string FileExtension => "xmind";

        /// <inheritdoc/>
        public List<MapData> ExtractData(FileInfo[] mindMaps)
        {
            var result = new List<MapData>();
            this.logger.LogInformation("starting extarting all deta data");
            if (mindMaps == null) throw new ArgumentNullException("no files with maps provided");

            foreach (var fileInfo in mindMaps)
            {
                this.logger.LogInformation($"opening file {fileInfo.Name}");

                try
                {
                    if (this.archiveExctractor.ArchiveHasFile(fileInfo, InsideFileJson))
                    {
                        var mapContent = this.archiveExctractor.ReadFileFromArchive(fileInfo, InsideFileJson);
                        result.Add(this.ParseJson(fileInfo, mapContent));
                    }
                    else if (this.archiveExctractor.ArchiveHasFile(fileInfo, InsideFileXml))
                    {
                        var mapContent = this.archiveExctractor.ReadFileFromArchive(fileInfo, InsideFileXml);
                        result.Add(this.ParseXml(fileInfo, mapContent));
                    }
                    else
                    {
                        var message =
                            $"Map doesnt containt {InsideFileJson} nor {InsideFileXml} skiping file {fileInfo.Name}";
                        this.logger.LogError(message);
                    }
                }
                catch (Exception e)
                {
                    this.logger.LogError(
                        $"Unexpected error occured while parsing file {fileInfo} details \n {e.Message}");
                }

                this.logger.LogInformation($"getting data from inside {InsideFileJson}");
            }

            this.logger.LogInformation($"finished extracting all data data");
            return result;
        }

        private MapData ParseJson(FileInfo fileInfo, string mapContent)
        {
            logger.LogDebug($"extracting nodes from file {InsideFileJson}");
            var nodesWithText = mapContent.Split(",")
                .ToList()
                .Where(t => t.Contains("title", StringComparison.Ordinal));
            var searchAbleContent = new List<string>();
            foreach (var node in nodesWithText)
            {
                this.logger.LogDebug(node);
                var extractValue = this.ExtractValue(node);
                this.logger.LogDebug($"Adding {extractValue} to results");
                searchAbleContent.Add(extractValue);
            }

            return new MapData { FileName = fileInfo.Name, FullName = fileInfo.FullName, Content = searchAbleContent };
        }

        private MapData ParseXml(FileInfo fileInfo, string mapContent)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(mapContent);
            var elementsByTagName = doc.GetElementsByTagName("tittle");
            var result = new List<string>();
            for (int i = 0; i < elementsByTagName.Count; i++)
            {
                result.Add(elementsByTagName[i].InnerText);
            }

            this.logger.LogTrace($"finished extrating data from {fileInfo.Name}");

            return new MapData { FileName = fileInfo.Name, FullName = fileInfo.FullName, Content = result };
        }

        private string ExtractValue(string valueWithFluff)
        {
            this.logger.LogTrace("removing unnecessary info");
            //// value has format  "title":"xxxx" sp split by : is good way to take only value
            return valueWithFluff.Split(":")[1].Trim();
        }
    }
}