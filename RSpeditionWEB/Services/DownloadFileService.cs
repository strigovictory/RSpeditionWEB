namespace RSpeditionWEB.Services
{
    internal class DownloadFileService : IDownloadFileService
    {
        /// <summary>
        /// The javascript runtime
        /// </summary>
        protected IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Constructor with the javascript runtime from IOC
        /// </summary>
        /// <param name="jSRuntime">The javascript runtime</param>
        public DownloadFileService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        /// <summary>
        /// Download a file from blazor context to the browser 
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <param name="bytesBase64">The bytes base 64 of the file</param>
        /// <param name="contentType">The file content type</param>
        /// <returns></returns>
        public async Task DownloadFile(string fileName, string bytesBase64, string contentType = "application/octet-stream")
        {
            Serilog.Log.Debug($"Запуск метода «{nameof(DownloadFile)}», отвечающего за загрузку файла размером {bytesBase64?.Length} символов.");
            try
            {
                await JSRuntime.InvokeVoidAsync("eval", DownloadFileScript.DownloadFileJavascriptScript(fileName, bytesBase64, contentType));
            }
            catch(Exception exc)
            {
                var mess = $"Исключительная ситуация при выполнении метода «{nameof(DownloadFile)}»," +
                    $" отвечающего за скачивание файла. Подробности: {exc.Message ?? string.Empty}";
                Serilog.Log.Error(mess);
                throw new Exception(mess);
            }
        }

        /// <summary>
        /// Download a file from blazor context to the browser
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <param name="bytes">The bytes of the file</param>
        /// <param name="contentType">The file content type</param>
        /// <returns></returns>
        public async Task DownloadFile(string fileName, byte[] bytes, string contentType = "application/octet-stream")
        {
            Serilog.Log.Debug($"Запуск метода «{nameof(DownloadFile)}», отвечающего за загрузку файла размером {bytes?.Length} элементов.");
            try
            {
                await JSRuntime.InvokeVoidAsync("eval", DownloadFileScript.DownloadFileJavascriptScript(fileName, Convert.ToBase64String(bytes), contentType));
            }
            catch (Exception exc)
            {
                var mess = $"Исключительная ситуация при выполнении метода «{nameof(DownloadFile)}»," +
                    $" отвечающего за скачивание файла. Подробности: {exc.Message ?? string.Empty}";
                Serilog.Log.Error(mess);
                throw new Exception(mess);
            }
        }

        /// <summary>
        ///  Download a file from blazor context to the browser
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <param name="stream">The stream of the file</param>
        /// <param name="contentType">The file content type</param>
        /// <returns></returns>
        public async Task DownloadFile(string fileName, Stream stream, string contentType = "application/octet-stream")
        {
            Serilog.Log.Debug($"Запуск метода «{nameof(DownloadFile)}», отвечающего за загрузку файла размером {stream?.Length} байт.");
            try
            {
                await JSRuntime.InvokeVoidAsync("eval", DownloadFileScript.DownloadFileJavascriptScript(fileName, Convert.ToBase64String(stream.ToByteArray()), contentType));
            }
            catch (Exception exc)
            {
                var mess = $"Исключительная ситуация при выполнении метода «{nameof(DownloadFile)}»," +
                    $" отвечающего за скачивание файла. Подробности: {exc.Message ?? string.Empty}";
                Serilog.Log.Error(mess);
                throw new Exception(mess);
            }
        }
    }
}
