namespace RSpeditionWEB.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="propType"></param>
        /// <returns></returns>
        public static string GetStringValue(this JsonElement elem, Type propType)
        {
            var res = string.Empty;

            switch (elem.ValueKind)
            {
                case (JsonValueKind.String):
                    res = elem.GetString();
                    break;
                case (JsonValueKind.True):
                    res = "+";
                    break;
                case (JsonValueKind.False):
                    res = "-";
                    break;
                case (JsonValueKind.Undefined):
                    res = string.Empty;
                    break;
                case (JsonValueKind.Null):
                    res = string.Empty;
                    break;
                case (JsonValueKind.Number):
                    res = GetStringNumber(propType);
                    break;
                default:
                    res = string.Empty;
                    break;
            }

            string GetStringNumber(Type propType)
            {
                var resInner = string.Empty;

                if (propType == typeof(Int16))
                {
                    resInner = elem.GetInt16().ToString();
                }
                else if (propType == typeof(UInt16))
                {
                    resInner = elem.GetUInt16().ToString();
                }
                else if (propType == typeof(Int32))
                {
                    resInner = elem.GetInt32().ToString();
                }
                else if (propType == typeof(UInt32))
                {
                    resInner = elem.GetUInt32().ToString();
                }
                else if (propType == typeof(Int64))
                {
                    resInner = elem.GetInt64().ToString();
                }
                else if (propType == typeof(UInt64))
                {
                    resInner = elem.GetUInt64().ToString();
                }
                else if (propType == typeof(double))
                {
                    resInner = elem.GetDouble().ToString();
                }
                else if (propType == typeof(decimal))
                {
                    resInner = elem.GetDecimal().ToString();
                }
                else if (Nullable.GetUnderlyingType(propType) == typeof(Int32))
                {
                    resInner = elem.GetInt32().ToString();
                }
                return resInner;
            }

            return res;
        }
    }
}
