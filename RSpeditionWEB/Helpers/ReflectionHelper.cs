namespace RSpeditionWEB.Helpers
{
    public static class ReflectionHelper
    {
        public static List<PropertyInfo> GetPropertiesSomeType(this Type typeItem) 
            => typeItem?
                                .GetProperties()?
                                .Where(_ => (_.PropertyType.IsValueType
                                            ||
                                            _.PropertyType.IsPrimitive
                                            ||
                                            _.PropertyType.IsEnum
                                            ||
                                            _.PropertyType == string.Empty.GetType()
                                            ||
                                            _.PropertyType == DateTime.Now.GetType())
                                )?
                                .ToList()
                                ??
                                new()
                                ;

        public static bool IsNullable(this PropertyInfo propInfo)
            => (propInfo?.GetGetMethod()?.ReturnType?.IsCollectible ?? false)
            || (propInfo?.GetGetMethod()?.ReturnType?.IsArray ?? false)
            || (propInfo?.GetGetMethod()?.ReturnType?.IsClass ?? false)
            || System.Nullable.GetUnderlyingType(propInfo?.GetGetMethod()?.ReturnType ?? default) != null
            || propInfo?.GetGetMethod()?.ReturnType == typeof(System.String);

        public static bool IsAbstract(this PropertyInfo propInfo)
            => propInfo?.GetGetMethod()?.ReturnType?.IsAbstract ?? false;
    }
}
