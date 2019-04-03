using ThirdPartyTools;

namespace FileInfoAPI
{
    public class FileInfoService : IFileInfoService
    {
        /// <summary>
        /// Service method for returning information about a gile.
        /// </summary>
        /// <param name="fitePath"></param>
        /// <returns></returns>
        public FileInfo GetFileInfo(string filePath)
        {
            var dates = new FileDates(filePath);
            var details = new FileDetails();

            return new FileInfo { Created = dates.Created, Modified = dates.Modified, Size = details.Size(filePath), Version = details.Version(filePath) };
        }
    }
}
