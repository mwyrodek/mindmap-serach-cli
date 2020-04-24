using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using mindmap_search.Model;

namespace mindmap_search.FileTypes
{
    public class XmindType : IMindMapType
    {
        private ILogger _logger;
        private string InsideFileJson = "content.json";
        private string InsideFileXml = "content.xml";
        private  IExctrectFromArchive _archiveExctractor;

        public XmindType(ILogger<XmindType> logger, IExctrectFromArchive archiveExctractor)
        {
            _logger = logger;
            _archiveExctractor = archiveExctractor;
        }
        
        public string FileExtension => "xmind";

        public List<MapData> ExtractData( FileInfo[] mindMaps)
        {
            List<MapData> result = new List<MapData>();
            _logger.LogInformation($"starting extarting all deta data");
            foreach (var fileInfo in mindMaps)
            {
                _logger.LogInformation($"opening file {fileInfo.Name}");
                
                try
                {
                    if(_archiveExctractor.ArchiveHasFile(fileInfo,InsideFileJson))
                    {
                        var mapcontent = _archiveExctractor.ReadFileFromArchive(fileInfo, InsideFileJson);
                        result.Add(ParseJson(fileInfo, mapcontent));
                    }
                    else if (_archiveExctractor.ArchiveHasFile(fileInfo,InsideFileXml))
                    {
                        var mapcontent = _archiveExctractor.ReadFileFromArchive(fileInfo, InsideFileXml);
                        result.Add(ParseXml(fileInfo, mapcontent));
                    }
                    else
                    {
                        var message =
                            $"Map doesnt containt {InsideFileJson} nor {InsideFileXml} skiping file {fileInfo.Name}";
                        _logger.LogError(message);
                    }
                }
                catch (Exception e) //todo narrow down count and types of exceptions
                {
                    _logger.LogError($"Unexpected error occured while parsing file {fileInfo} details \n {e.Message}");
                }
                
                _logger.LogInformation($"getting data from inside {InsideFileJson}");
            }
            _logger.LogInformation($"finished extracting all data data");
            return result;
        }

        private MapData ParseJson(FileInfo fileInfo, string mapContent)
        {
            _logger.LogDebug($"extracting nodes from file {InsideFileJson}");
            var nodesWithText = mapContent.Split(",")
                .ToList()
                .Where(t => t.Contains("title"));
            var searchAbleContent = new List<string>();
            foreach (var node in nodesWithText)
            {
                _logger.LogDebug(node);
                var extratValue = ExtratValue(node);
                _logger.LogDebug($"Adding {extratValue} to results");
                    
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


            _logger.LogTrace($"finished extrating data from {fileInfo.Name}");
            
            return new MapData{FileName = fileInfo.Name, FullName = fileInfo.FullName, Content = result};
        }

        private string ExtratValue(string valueWithFluff)
        {
            _logger.LogTrace($"removing unnesecery info");
            // value has format  "title":"xxxx" sp split by : is good way to take only value
            return valueWithFluff.Split(":")[1].Trim();

        }
    }
}