namespace RSpeditionWEB.Helpers
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Метод для конвертации коллекции типа object в тип IId
        /// </summary>
        /// <param name="classType">Тип класса</param>
        /// <param name="objItems">Коллекция типа object,подлежащая преобразованию</param>
        /// <returns>Коллекция экземпляров-наследников интерфейса IId</returns>
        public static List<IId> ConvertObjToIIdItems(this List<object> objItems, Type classType)
        {
            List<IId> res = new();
            if(objItems != null)
            {
                foreach(var obj in objItems)
                {
                    var itemStr = obj?.ToString() ?? String.Empty;
                    var item = JsonSerializer.Deserialize(itemStr, classType);
                    if (item != null && item is IId)
                    {
                        res.Add((IId)item);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Метод для конвертации коллекции типа IId в тип  object 
        /// </summary>
        /// <param name="iIdItems">Коллекция типа IId,подлежащая преобразованию</param>
        /// <returns>Коллекция экземпляров-наследников интерфейса object</returns>
        public static List<object> ConvertIIdToObjItems(this List<IId> iIdItems)
        {
            List<object> res = new();
            foreach (var iid in iIdItems)
            {
                var itemStr = Newtonsoft.Json.JsonConvert.SerializeObject(iid);
                if (!string.IsNullOrEmpty(itemStr))
                {
                    res.Add(itemStr);
                }
            }

            return res;
        }
    }
}
