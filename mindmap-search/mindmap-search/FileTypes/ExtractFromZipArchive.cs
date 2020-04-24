using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Transactions;
using Microsoft.Extensions.Logging;

namespace mindmap_search.FileTypes
{
    public class ExtractFromZipArchive : IExctrectFromArchive
    {
        
        private ILogger _logger;
        public ExtractFromZipArchive(ILogger<ExtractFromZipArchive> logger)
        {
            _logger = logger;
        }

        public bool ArchiveHasFile(FileInfo info, string fileName)
        {
            using var zipToOpen = new FileStream(info.FullName, FileMode.Open);
            using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);            
            _logger.LogDebug($"Checking if file {fileName} is in map files");
            return archive.Entries.Any(s => s.Name == fileName);
             
        }

        public string ReadFileFromArchive(FileInfo info, string fileName)
        {
            using var zipToOpen = new FileStream(info.FullName, FileMode.Open);
            using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read); 
            
            var readmeEntry = archive.GetEntry(fileName);
            using var reader= new StreamReader(readmeEntry.Open());
            return reader.ReadToEnd();
        }
    }
}