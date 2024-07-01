namespace RSpeditionWEB.Interfaces
{
    public interface IDownloadFileService
    {
        /// <summary>
        /// Download a file from blazor context to the browser. 
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <param name="bytesBase64">The bytes base 64 of the file</param>
        /// <param name="contentType">The file content type</param>
        /// <returns></returns>
        Task DownloadFile(string fileName, string bytesBase64, string contentType = "application/octet-stream");

        /// <summary>
        /// Download a file from blazor context to the browser.
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <param name="bytes">The bytes of the file</param>
        /// <param name="contentType">The file content type</param>
        /// <returns></returns>
        Task DownloadFile(string fileName, byte[] bytes, string contentType = "application/octet-stream");

        /// <summary>
        ///  Download a file from blazor context to the browser.
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <param name="stream">The stream of the file</param>
        /// <param name="contentType">The file content type</param>
        /// <returns></returns>
        Task DownloadFile(string fileName, Stream stream, string contentType = "application/octet-stream");
    }
}
