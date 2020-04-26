using System.Collections.Generic;
using System.IO;
using mindmap_search.Model;

namespace mindmap_search.FolderCrawl
{
    public interface IExtractInfoFromMaps
    {
        List<MapData> ExtartInfo(FileInfo[] fileInfos);
    }
}