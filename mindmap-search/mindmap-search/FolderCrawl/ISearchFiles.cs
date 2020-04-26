namespace mindmap_search.FolderCrawl
{
    using System.IO;

    /// <summary>
    /// Interface for searching given directory for mind maps of given type
    /// </summary>
    public interface ISearchFiles
    {
        /// <summary>
        /// Searches for maps in currect directory
        /// </summary>
        /// <param name="rootPath">adress of folder to serch.</param>
        /// <returns>all mind maps of given format.</returns>
        FileInfo[] FindAllMaps(string rootPath);
    }
}