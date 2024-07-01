using RSpeditionWEB.Components.Generic;

namespace RSpeditionWEB.Helpers
{
    public static class ForeachHelper<T, V> where T : class where V : class
    {
        public static List<V> MapItemsList(List<T> items, Func<T, V> mapper)
        {
            ConcurrentBag<V> res = new();

            var result = Parallel.ForEach(items ?? new(),
                                          new ParallelOptions { MaxDegreeOfParallelism = 32 },
                                           item =>
                                           {
                                               res.Add(mapper(item));
                                           });
            if (!result.IsCompleted)
            {
                Serilog.Log.Error($"Ошибка в методе {nameof(MapItemsList)} " +
                                   $"Цикл был прерван! Выполнение завершено на итерации {result.LowestBreakIteration ?? 0}");
            }

            return res?.ToList() ?? new();
        }
    }
}
