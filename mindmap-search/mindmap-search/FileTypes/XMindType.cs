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

        public XMindType(ILogger<XMindType> logger, IExctrectFromArchive archiveExctractor)
        {
            this.logger = logger;
            this.archiveExctractor = archiveExctractor;
        }

        public string FileExtension => "xmind";

        public List<MapData> ExtractData( FileInfo[] mindMaps)
        {
            List<MapData> result = new List<MapData>();
            logger.LogInformation($"starting extarting all deta data");
            foreach (var fileInfo in mindMaps)
            {
                logger.LogInformation($"opening file {fileInfo.Name}");
                
                try
                {
                    if(archiveExctractor.ArchiveHasFile(fileInfo,InsideFileJson))
                    {
                        var mapcontent = archiveExctractor.ReadFileFromArchive(fileInfo, InsideFileJson);
                        result.Add(ParseJson(fileInfo, mapcontent));
                    }
                    else if (archiveExctractor.ArchiveHasFile(fileInfo,InsideFileXml))
                    {
                        var mapcontent = archiveExctractor.ReadFileFromArchive(fileInfo, InsideFileXml);
                        result.Add(ParseXml(fileInfo, mapcontent));
                    }
                    else
                    {
                        var message =
                            $"Map doesnt containt {InsideFileJson} nor {InsideFileXml} skiping file {fileInfo.Name}";
                        logger.LogError(message);
                    }
                }
                catch (Exception e) //todo narrow down count and types of exceptions
                {
                    logger.LogError($"Unexpected error occured while parsing file {fileInfo} details \n {e.Message}");
                }
                
                logger.LogInformation($"getting data from inside {InsideFileJson}");
            }
            logger.LogInformation($"finished extracting all data data");
            return result;
        }

        private MapData ParseJson(FileInfo fileInfo, string mapContent)
        {
            logger.LogDebug($"extracting nodes from file {InsideFileJson}");
            var nodesWithText = mapContent.Split(",")
                .ToList()
                .Where(t => t.Contains("title"));
            var searchAbleContent = new List<string>();
            foreach (var node in nodesWithText)
            {
                logger.LogDebug(node);
                var extratValue = ExtratValue(node);
                logger.LogDebug($"Adding {extratValue} to results");
                    
                searchAbleContent.Add(extratValue);
                    
            }

            return new MapData{FileName = fileInfo.Name, FullName = fileInfo.FullName, Content = searchAbleContent};
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


            logger.LogTrace($"finished extrating data from {fileInfo.Name}");
            
            return new MapData{FileName = fileInfo.Name, FullName = fileInfo.FullName, Content = result};
        }

        private string ExtratValue(string valueWithFluff)
        {
            logger.LogTrace($"removing unnesecery info");
            // value has format  "title":"xxxx" sp split by : is good way to take only value
            return valueWithFluff.Split(":")[1].Trim();

        }
    }
}