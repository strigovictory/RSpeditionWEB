namespace RSpeditionWEB.Helpers
{
    // Класс добавляет несколько методов расширения в диспетчер навигации для доступа к параметрам строки запроса в URL-адресе.
    public static class NavigationManagerExtension
    {
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }
    }
}
