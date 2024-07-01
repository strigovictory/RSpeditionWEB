using RSpeditionWEB.Helpers.Attributes;

namespace RSpeditionWEB.Helpers
{
    public static class AttributesHelper
    {
        /// <summary>
        /// Метод для получения значения Display-атрибута модели
        /// </summary>
        /// <param name="model">Класс, в котором применен атрибут</param>
        /// <param name="propertyname">Свойство класса, к которому применен атрибут</param>
        /// <returns>Наименование таблицы</returns>
        public static string GetDisplayAttributeValues(this Type model, string propertyname)
        {
            string name = string.Empty;
            MemberInfo myprop = model?.GetProperty(propertyname) as MemberInfo;

            if (myprop != null)
            {
                var attr = myprop.GetCustomAttribute(typeof(DisplayAttribute));

                if (attr == null)
                {
                    attr = myprop.GetCustomAttribute(typeof(CustomDisplayAttribute));
                    if (attr != null)
                    {
                        var displayAttr = attr as CustomDisplayAttribute;
                        name = displayAttr?.DisplayName?.ToString() ?? string.Empty;
                    }
                }
                else
                {
                    var displayAttr = attr as DisplayAttribute;
                    name = displayAttr?.Name?.ToString() ?? string.Empty;
                }
            }

            return name;
        }

        public static string GetModelsDisplayNameValue(this Type model)
            => model?.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name ?? string.Empty;

        public static (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) GetValuesForInputSearch(this Type itemsType)
            => itemsType.Name switch
            {
                nameof(CountryResponse) => GetValuesForInputSearch_Country(),
                nameof(CurrencyResponse) => GetValuesForInputSearch_Currancy(),
                nameof(FuelTypeResponse) => GetValuesForInputSearch_TRideCostCategory(),
                nameof(DivisionResponse) => GetValuesForInputSearch_TDivision(),
                nameof(FuelCardFullModel) => GetValuesForInputSearch_FuelCard_View(),
                nameof(FuelProviderResponse) => GetValuesForInputSearch_TFuelCardType(),
                _ => default
            };

        public static (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) GetValuesForInputSearch_TFuelCardType()
        {
            List<PropertyInfo> props = new();
            PropertyInfo textProp = default;
            string labelForSearch = string.Empty;
            string placeholder = string.Empty;
            PropertyInfo keyPropInfo = default;

            var item = (FuelProviderResponse)Activator.CreateInstance(typeof(FuelProviderResponse));
            var type = item.GetType();
            props = new List<PropertyInfo> {type.GetProperty(nameof(item.Name))};
            textProp = type.GetProperty(nameof(item.Name));
            var attr = type.GetCustomDisplayValues(nameof(item.Name));
            labelForSearch = attr.Item1 ?? string.Empty;
            placeholder = attr.Item2 ?? string.Empty;
            keyPropInfo = type.GetProperty(nameof(item.Id));
            return (props, textProp, labelForSearch, placeholder, keyPropInfo);
        }

        public static (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) GetValuesForInputSearch_Country()
        {
            List<PropertyInfo> props = new();
            PropertyInfo textProp = default;
            string labelForSearch = string.Empty;
            string placeholder = string.Empty;
            PropertyInfo keyPropInfo = default;

            var item = (CountryResponse)Activator.CreateInstance(typeof(CountryResponse));
            var type = item.GetType();
            props = new List<PropertyInfo> {type.GetProperty(nameof(item.Code)),
                                            type.GetProperty(nameof(item.Name)),
                                            type.GetProperty(nameof(item.NameEng))};
            textProp = type.GetProperty(nameof(item.Name));
            var attr = type.GetCustomDisplayValues(nameof(item.Name));
            labelForSearch = attr.Item1 ?? string.Empty;
            placeholder = attr.Item2 ?? string.Empty;
            keyPropInfo = type.GetProperty(nameof(item.Id));
            return (props, textProp, labelForSearch, placeholder, keyPropInfo);
        }

        public static (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) GetValuesForInputSearch_Currancy()
        {
            List<PropertyInfo> props = new();
            PropertyInfo textProp = default;
            string labelForSearch = string.Empty;
            string placeholder = string.Empty;
            PropertyInfo keyPropInfo = default;

            var item = (CurrencyResponse)Activator.CreateInstance(typeof(CurrencyResponse));
            var type = item.GetType();
            props = new List<PropertyInfo> {type.GetProperty(nameof(item.Name)),
                                            type.GetProperty(nameof(item.FullNameEng)),
                                            type.GetProperty(nameof(item.Code))};
            textProp = type.GetProperty(nameof(item.Name));
            var attr = type.GetCustomDisplayValues(nameof(item.Name));
            labelForSearch = attr.Item1 ?? string.Empty;
            placeholder = attr.Item2 ?? string.Empty;
            keyPropInfo = type.GetProperty(nameof(item.Id));

            return (props, textProp, labelForSearch, placeholder, keyPropInfo);
        }

        public static (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) GetValuesForInputSearch_TRideCostCategory()
        {
            List<PropertyInfo> props = new();
            PropertyInfo textProp = default;
            string labelForSearch = string.Empty;
            string placeholder = string.Empty;
            PropertyInfo keyPropInfo = default;

            var item = (FuelTypeResponse)Activator.CreateInstance(typeof(FuelTypeResponse));
            var type = item.GetType();
            props = new List<PropertyInfo> {type.GetProperty(nameof(item.Name))};
            textProp = type.GetProperty(nameof(item.Name));
            var attr = type.GetCustomDisplayValues(nameof(item.Name));
            labelForSearch = attr.Item1 ?? string.Empty;
            placeholder = attr.Item2 ?? string.Empty;
            keyPropInfo = type.GetProperty(nameof(item.Id));

            return (props, textProp, labelForSearch, placeholder, keyPropInfo);
        }

        public static (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) GetValuesForInputSearch_TDivision()
        {
            List<PropertyInfo> props = new();
            PropertyInfo textProp = default;
            string labelForSearch = string.Empty;
            string placeholder = string.Empty;
            PropertyInfo keyPropInfo = default;

            var item = (DivisionResponse)Activator.CreateInstance(typeof(DivisionResponse));
            var type = item.GetType();
            props = new List<PropertyInfo> { type.GetProperty(nameof(item.Name)) };
            textProp = type.GetProperty(nameof(item.Name));
            var attr = type.GetCustomDisplayValues(nameof(item.Name));
            labelForSearch = attr.Item1 ?? string.Empty;
            placeholder = attr.Item2 ?? string.Empty;
            keyPropInfo = type.GetProperty(nameof(item.Id));

            return (props, textProp, labelForSearch, placeholder, keyPropInfo);
        }

        public static (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) GetValuesForInputSearch_FuelCard_View()
        {
            List<PropertyInfo> props = new();
            PropertyInfo textProp = default;
            string labelForSearch = string.Empty;
            string placeholder = string.Empty;
            PropertyInfo keyPropInfo = default;

            var item = (FuelCardFullModel)Activator.CreateInstance(typeof(FuelCardFullModel));
            var type = item.GetType();
            props = new List<PropertyInfo> { type.GetProperty(nameof(item.Number)) };
            textProp = type.GetProperty(nameof(item.Number));
            var attr = type.GetCustomDisplayValues(nameof(item.Number));
            labelForSearch = attr.Item1 ?? string.Empty;
            placeholder = attr.Item2 ?? string.Empty;
            keyPropInfo = type.GetProperty(nameof(item.Id));

            return (props, textProp, labelForSearch, placeholder, keyPropInfo);
        }

        public static (string, string) GetCustomDisplayValues(PropertyInfo prop)
        {
            string name = string.Empty;
            string mess = string.Empty;

            if (prop != null)
            {
                var attr = prop.GetCustomAttribute(typeof(CustomDisplayAttribute));

                if (attr != null)
                {
                    var customDisplayAttr = attr as CustomDisplayAttribute;

                    name = customDisplayAttr?.DisplayName?.ToString() ?? string.Empty;
                    mess = customDisplayAttr?.RequiredMessage?.ToString() ?? string.Empty;
                }
            }

            return (name, mess);
        }

        /// <summary>
        /// Метод для получения значений атрибутов свойств модели
        /// </summary>
        /// <param name="model">Класс, в котором применены атрибуты</param>
        /// <param name="propertyname">Свойство класса, к которому применен атрибут</param>
        /// <returns>Кортеж значений атрибута - Name1 & Name2</returns>
        public static (string, string) GetCustomDisplayValues(this Type model, string propertyname)
        {
            string name = string.Empty;
            string mess = string.Empty;

            if (model is null) return (name, mess);

            var myprop = model.GetProperty(propertyname) as MemberInfo;

            if (myprop != null)
            {
                var attr = myprop.GetCustomAttribute(typeof(CustomDisplayAttribute));

                if (attr != null)
                {
                    var customDisplayAttr = attr as CustomDisplayAttribute;

                    name = customDisplayAttr?.DisplayName?.ToString() ?? string.Empty;
                    mess = customDisplayAttr?.RequiredMessage?.ToString() ?? string.Empty;
                }
            }

            return (name, mess);
        }

        /// <summary>
        /// Метод для получения значений атрибута CustomDisplayAttribute, примененного к классу 
        /// </summary>
        /// <param name="model">Класс, к которому применены атрибут CustomDisplayAttribute</param>
        /// <returns>Коллекция значений атрибута CustomDisplayAttribute</returns>
        public static List<string> GetCustomDisplayValues(this Type model)
        {
            List<string> res = new();

            if (model != null)
            {
                var attr = model.GetCustomAttribute(typeof(CustomDisplayAttribute));

                if (attr != null)
                {
                    var customDisplayAttr = attr as CustomDisplayAttribute;
                    res = customDisplayAttr?.CustomDisplayAttributes ?? new();
                }
            }

            return res;
        }
    }
}
