using System.Collections.Generic;
using mindmap_search.Model;

namespace mindmap_search.Engine
{
    public interface IEngine
    {
        void FindMaps(string path);
        List<SearchResult> Search(string querry);
    }
}