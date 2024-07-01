namespace RSpeditionWEB.Helpers
{
    public static class MapperHelper
    {
        static ConcurrentBag<object> Items;

        public static List<object> MapTo(this IMapper mapper, Serilog.ILogger logger, List<object> source, Type sourceType, Type destinationType)
        {
            Items = new();

            try
            {
                var res = Parallel.ForEach(source,
                                                    new ParallelOptions { MaxDegreeOfParallelism = 32 },
                                                    (item) =>
                                                    {
                                                        var mappedItem = mapper?.Map(item, sourceType, destinationType);
                                                        Items?.Add(mappedItem);
                                                    });

                if (!res.IsCompleted)
                    throw new Exception($"Ошибка при конвертации коллекции экземпляров ({source?.Count ?? 0} шт.) типа {sourceType} в тип {destinationType}");

            }
            catch (Exception exc)
            {
                exc?.LogError(logger, nameof(MapperHelper), nameof(MapTo));
            }

            return Items?.ToList() ?? new();
        }

        public static async Task<List<object>> MapToAsync(this IMapper mapper, Serilog.ILogger logger, List<object> source, Type sourceType, Type destinationType)
        {
            Items = new();

            try
            {
               await Parallel.ForEachAsync(source,
                                           new ParallelOptions { MaxDegreeOfParallelism = 32 },
                                           async (item, token) =>
                                                    {
                                                        var mappedItem = mapper?.Map(item, sourceType, destinationType);
                                                        Items?.Add(mappedItem);
                                                        await Task.Delay(1);
                                                    });

                if (Items == null)
                    throw new Exception($"Ошибка при конвертации коллекции экземпляров ({source?.Count ?? 0} шт.) типа {sourceType} в тип {destinationType}");

            }
            catch (Exception exc)
            {
                exc?.LogError(logger, nameof(MapperHelper), nameof(MapTo));
            }

            return Items?.ToList() ?? new();
        }
    }
}
