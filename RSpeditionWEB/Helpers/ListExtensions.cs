namespace RSpeditionWEB.Helpers
{
    public static class ListExtensions
    {
        /// <summary>
        /// Метод для преобразования коллекции в строку
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ItemsToString<T>(this List<T> list, string divider)
        {
            string result = "";
            foreach (var item in list)
            {
                result += $"{item.ToString()} {divider} ";
            }
            return result;
        }

        /// <summary>
        /// Метод для преобразования коллекции в строку
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ItemsToString<T>(this List<T> list, string divider, string marker)
        {
            string result = "";
            foreach (var item in list)
            {
                result += $"{marker} {item.ToString()} {divider} ";
            }
            return result;
        }
    }
}
