namespace RSpeditionWEB.Helpers
{
    public static class SearchExtention
    {
        /// <summary>
        /// Метод для ограничения заданной коллекции экземпляров IId поисковой строкой
        /// </summary>
        /// <param name="items">Коллекция в рамках которой осуществляется поиск</param>
        /// <param name="props">Свойства, в рамках значений которых которых будет осуществляться поиск вхождения заданной строки. Свойства должны быть строкового типа !!!</param>
        /// <param name="searchString">поисковая строка</param>
        /// <returns>Коллекция экземпляров IId отвечающая условиям поисковой строкой</returns>
        public static List<IId> SearchedIIdItems(this List<IId> items, List<PropertyInfo> props, string searchString = "")
        {
            List<IId> res = new();

            foreach(var item in items)
            {
                foreach(var prop in props)
                {
                    var propValue = prop?.GetValue(item)?.ToString()?.Trim();

                    if(propValue != null &&
                       propValue.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) &&
                       !res.Contains(item))
                    {
                        res.Add(item);
                        break; // перейти к следующему экзумпляру при первом вхождении поисковой строки со значеним одного из свойств
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Метод для ограничения заданной коллекции экземпляров IId поисковой строкой
        /// </summary>
        /// <param name="items">Коллекция в рамках которой осуществляется поиск</param>
        /// <param name="props">Свойства, в рамках значений которых которых будет осуществляться поиск вхождения заданной строки. Свойства должны быть строкового типа !!!</param>
        /// <param name="searchString">поисковая строка</param>
        /// <returns>Коллекция экземпляров IId отвечающая условиям поисковой строкой</returns>
        public static List<IIdString> SearchedIIdItems(this List<IIdString> items, List<PropertyInfo> props, string searchString = "")
        {
            List<IIdString> res = new();

            foreach (var item in items)
            {
                foreach (var prop in props)
                {
                    var propValue = prop?.GetValue(item)?.ToString()?.Trim();

                    if (propValue != null &&
                       propValue.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) &&
                       !res.Contains(item))
                    {
                        res.Add(item);
                        break; // перейти к следующему экзумпляру при первом вхождении поисковой строки со значеним одного из свойств
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return res;
        }
    }
}
