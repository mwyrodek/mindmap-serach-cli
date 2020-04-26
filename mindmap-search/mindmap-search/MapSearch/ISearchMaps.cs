using System.Collections.Generic;
using mindmap_search.Model;

namespace mindmap_search.MapSearch
{
    public interface ISearchMaps
    {
        List<SearchResult> FindNodesWithValue(string searchedText);
        void LoadMapsData(List<MapData> mapsData);
    }
}