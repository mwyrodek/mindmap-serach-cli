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

        public XmindType(ILogger<XmindType> logger)
        {
            _logger = logger;
        }
        public string FileExtension => "xmind";

        public List<MapData> ExtractData( FileInfo[] mindMaps)
        {
            List<MapData> result = new List<MapData>();
            _logger.LogInformation($"starting extarting all deta data");
            foreach (var fileInfo in mindMaps)
            {
                _logger.LogInformation($"opening file {fileInfo.Name}");
                using var zipToOpen = new FileStream(fileInfo.FullName, FileMode.Open);
                using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
                string entryToGet;
                try
                {
                    if (HasJsonFile(archive))
                    {
                        result.Add(ParseJson(archive, fileInfo));
                    }
                    else if (HasXmlFile(archive))
                    {
                        result.Add(ParseXml(archive, fileInfo));
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

        private bool HasJsonFile(ZipArchive archive)
        {
            _logger.LogDebug($"Checking if file {InsideFileJson} is in map files");
            return archive.Entries.Any(s => s.Name == InsideFileJson);
        }
        
        private MapData ParseJson(ZipArchive archive, FileInfo fileInfo)
        {
            
            var readmeEntry = archive.GetEntry(InsideFileJson);
            using var reader= new StreamReader(readmeEntry.Open());
            var readLine = reader.ReadLine();

            _logger.LogDebug($"extracting nodes from file {InsideFileJson}");
            var nodesWithText = readLine.Split(",")
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
        
        private MapData ParseXml(ZipArchive archive, FileInfo fileInfo)
        {
            _logger.LogTrace($"starting extracting data from {fileInfo.Name} as xml");
            
            var readmeEntry = archive.GetEntry(InsideFileXml);

            _logger.LogTrace($"starting extracting data from read entry {InsideFileXml}");
            using var reader = new StreamReader(readmeEntry.Open());
            var readLine = reader.ReadLine();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(readLine);
            var elementsByTagName = doc.GetElementsByTagName("tittle");
            var result = new List<string>();
            for (int i = 0; i < elementsByTagName.Count; i++)
            {
                result.Add(elementsByTagName[i].InnerText);
            }


            _logger.LogTrace($"finished extrating data from {fileInfo.Name}");
            
            return new MapData{FileName = fileInfo.Name, FullName = fileInfo.FullName, Content = result};
        }
        
        private bool HasXmlFile(ZipArchive archive)
        {
            _logger.LogTrace($"Checking if file {InsideFileXml} is in map files");
            return archive.Entries.Any(s => s.Name == InsideFileXml);
        }

        private string ExtratValue(string valueWithFluff)
        {
            _logger.LogTrace($"removing unnesecery info");
            // value has format  "title":"xxxx" sp split by : is good way to take only value
            return valueWithFluff.Split(":")[1].Trim();

        }
    }
}