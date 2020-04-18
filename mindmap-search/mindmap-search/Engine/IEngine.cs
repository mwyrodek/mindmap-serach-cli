using System.Collections.Generic;
using mindmap_search.Model;

namespace mindmap_search.Engine
{
    public interface IEngine
    {
        void LoadConfig();
        void LoadConfig(string configPath);
        void LoadCache();
        void FindMaps(string path);
        void FindMaps();
        List<SearchResult> Search(string querry);
        void OpenFirstResult(string querry);
    }
}