using System.IO;

namespace mindmap_search.FolderCrawl
{
    public interface ISearchFiles
    {
        FileInfo[] FindAllMaps(string rootPath);
    }
}