using System.Collections.Generic;
using System.IO;
using mindmap_search.Model;

namespace mindmap_search.FileTypes
{
    public interface IMindMapType
    {
        public string FileExtension { get; }

        public List<MapData> ExtractData(FileInfo[] mindMaps);
    }
}