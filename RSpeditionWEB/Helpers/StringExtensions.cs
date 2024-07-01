namespace RSpeditionWEB.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Метод для преобразования строки в коллекцию строк
        /// </summary>
        /// <param name="str">Первоначальная строка</param>
        /// <returns>Коллекция строк</returns>
        public static List<string> ConvertStringToList(this string str, char divider) => str
                                                                                            .Split(divider)
                                                                                            .ToList<string>()?
                                                                                            .Select(_ => _.Trim())?
                                                                                            .Where(_ => !string.IsNullOrEmpty(_))?
                                                                                            .ToList()
                                                                                            ??
                                                                                            new()
                                                                                            ;
        /// <summary>
        /// Метод для преобразования коллекции строк в строку
        /// </summary>
        /// <param name="str">Первоначальная коллекция строка</param>
        /// <returns>Cтрока</returns>
        public static string ConvertListToString(this List<string> collection)
            => new string(collection?.SelectMany(_ => " " + _)?.ToArray() ?? new char[0]);

        public static string ConvertListToString(this List<string> collection, char divider)
            => new string(collection.SelectMany(_ => _ + divider)?.ToArray() ?? new char[0]);

        public static string ConvertStringUpperToLowerCase(this string str)
        {
            string res = string.Empty;
            var ind = -1;

            if(!string.IsNullOrEmpty(str))
            {
                foreach (char letter in str)
                {
                    ind++;

                    if (!Char.IsUpper(letter))
                    {
                        res += letter;
                    }
                    else
                    {
                        if (ind != 0)
                        {
                            res += (string.Concat(" ", letter)).ToLower();
                        }
                        else
                        {
                            res += string.Concat(" ", letter);
                        }
                    }
                }
            }

            return res?.ToLower() ?? string.Empty;
        }
    }
}
