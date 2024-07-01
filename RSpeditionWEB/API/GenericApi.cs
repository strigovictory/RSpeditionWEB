using RSpeditionWEB.Configs;
using static RSpeditionWEB.API.ApiPointsBase;

namespace RSpeditionWEB.API
{
    public class GenericApi : ApiPointsBase
    {
        private readonly Dictionary<Type, string> uriForTypes_get;
        private readonly Dictionary<Type, string> uriForTypes_post;
        private readonly Dictionary<Type, string> uriForTypes_put;
        private readonly Dictionary<Type, string> uriForTypes_del;
        public GenericApi(IHttpService httpService, IOptions<GateWayConfigurations> options) : base(httpService, options)
        {
            uriForTypes_get = InitUriForTypes(OperationsType.read);
            uriForTypes_put = InitUriForTypes(OperationsType.update);
            uriForTypes_post = InitUriForTypes(OperationsType.create);
            uriForTypes_del = InitUriForTypes(OperationsType.delete);
        }

        private Dictionary<Type, (ApiPointsBase.ControllersNames controllerName, string methodNameGet, string methodNamePut, string methodNamePost, string methodNameDelete)> ApiPoints
            => new Dictionary<Type, (ApiPointsBase.ControllersNames controllerName, string methodNameGet, string methodNamePut, string methodNamePost, string methodNameDelete)>
            {
                { typeof(TruckResponse), ((ApiPointsBase.ControllersNames.trucks), nameof(TruckApi.GetTrucks), string.Empty, string.Empty, string.Empty) },
            };

        public override ControllersNames ControllerName => throw new NotImplementedException();

        /// <summary>
        /// Метод для получения коллекции-словаря, в которой ключ - это тип экземпляра класса, а значение - это адрес для получения коллекции экземпляров данного типа
        /// </summary>
        /// <returns>Коллекция-словарь</returns>
        private Dictionary<Type, string> InitUriForTypes(OperationsType operationsType)
        {
            Dictionary<Type, string> res = new();

            switch (operationsType)
            {
                case OperationsType.read:
                {
                    break;
                }
                case OperationsType.update:
                {
                    break;
                }
                case OperationsType.create: 
                {
                        break;
                }
                case OperationsType.delete: 
                {
                        break;
                }
                default: break;
            }

            return res;
        }

        public async Task<List<T>> GetListObjItems<T>(CancellationToken token)
            => await http?.SendRequestAsync<List<T>>(
                uriForTypes_get[typeof(T)],
                HttpMethod.Get,
                token: token);

        public async Task<List<object>> GetListObjItems(Type typeClass, CancellationToken token)
            => await http?.SendRequestAsync<List<object>>(
                uriForTypes_get[typeClass],
                HttpMethod.Get, 
                token: token);

        public async Task<T> PostTItem<T>(T item, CancellationToken token)
            => await http?.SendRequestAsync<T, T>(
                uriForTypes_post[item.GetType()],
                HttpMethod.Post,
                item, 
                token);

        public async Task<T> PutTItem<T>(T item, CancellationToken token)
            => await http?.SendRequestAsync<T, T>(
                uriForTypes_put[item.GetType()],
                HttpMethod.Put,
               item, 
               token);

        public async Task<V> DelTItem<T, V>(T item, CancellationToken token)
            => await http?.SendRequestAsync<T, V>(
                uriForTypes_del[item.GetType()],
                HttpMethod.Post, 
                item, 
                token);
    }
}
