namespace mindmap_search.FileTypes
{
    using System.IO;

    /// <summary>
    /// Interface used to extract data from file archives
    /// </summary>
    public interface IExctrectFromArchive
    {
        /// <summary>
        /// Checks if archive has file
        /// </summary>
        /// <param name="info">name and location of archive.</param>
        /// <param name="fileName">file to find in archive.</param>
        /// <returns>true is file is present.</returns>
         bool ArchiveHasFile(FileInfo info, string fileName);

        /// <summary>
        /// Reads given file from archive.
        /// </summary>
        /// <param name="info">name and location of archive.</param>
        /// <param name="fileName">file to read in archive.</param>
        /// <returns>file value.</returns>
         string ReadFileFromArchive(FileInfo info, string fileName);
    }
}