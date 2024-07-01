using RSpeditionWEB.Enums.Graphics;

namespace RSpeditionWEB.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Вспомогательный метод для преобразования наименования перечисления в ее экземпляр
        /// </summary>
        /// <param name="enumType">Тип перечисления</param>
        /// <param name="name">Наименование перечисления</param>
        /// <returns>Экземпляр еречисления</returns>
        private static object GetSchemaNameByName(Type enumType, string name)
        {
            object res = null;

            if (Enum.TryParse(enumType, name?.Trim() ?? string.Empty, out var resParsing))
            {
                res = resParsing;
            }

            return res;
        }
    }
}
