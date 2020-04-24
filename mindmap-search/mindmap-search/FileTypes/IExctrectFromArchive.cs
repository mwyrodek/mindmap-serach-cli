using System;
using System.IO;
using mindmap_search.Model;

namespace mindmap_search.FileTypes
{
    public interface IExctrectFromArchive : IExtrectFromFile
    {
         bool ArchiveHasFile(FileInfo info, string fileName);
         
         string ReadFileFromArchive(FileInfo info, string fileName);
    }
}