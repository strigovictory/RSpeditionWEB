namespace RSpeditionWEB.Helpers
{
    public static class ExceptionHelper
    {
        public static string GetExcMessages(this Exception exc, string devider)
        => $"{exc?.Message ?? string.Empty}{devider}" +
           $"{exc?.InnerException?.Message ?? string.Empty}{devider}";

        public static void LogError(this Exception ex, Serilog.ILogger logger, string sourceName, string methodName = null)
        {
            if(string.IsNullOrEmpty(methodName))
            {
                logger.Error($"Исключительная ситуация в классе {sourceName ?? string.Empty} - " +
                             $"тип исключения: «{ex.GetType()?.FullName ?? string.Empty}», " +
                             $"подробности: {ex?.Message ?? string.Empty}");
            }
            else
            {
                logger.Error($"Исключительная ситуация при выполнении метода {methodName ?? string.Empty} " +
                             $"в классе {sourceName ?? string.Empty} - " +
                             $"тип исключения: «{ex.GetType()?.FullName ?? string.Empty}», " +
                             $"подробности: {ex?.Message ?? string.Empty}");
            }
        }

        public static void LogError(this Exception exc, string sourceName, string methodName, string message = null)
        {
            Log.Error($"Исключительная ситуация при выполнении метода «{methodName}» " +
                      $"в классе «{sourceName ?? string.Empty}». " +
                      $"Тип: «{exc?.GetType()?.FullName ?? string.Empty}». " +
                      $"Подробности: {message ?? string.Empty} " +
                      $"{exc.GetExeceptionMessages()} ");
        }

        public static string GetExeceptionMessages(this Exception exc)
        {
            return $"{exc?.Message ?? string.Empty} {exc?.InnerException?.Message ?? string.Empty}";
        }
    }
}
