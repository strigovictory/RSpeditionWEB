using RSpeditionWEB.Configs;
using RSpeditionWEB.Models.RequestModels;
using RSpeditionWEB.Models.Results;

namespace RSpeditionWEB.API
{
    // Класс, для поддержки всех видов httpService-запросов, касающихся учета топливных карт
    public class FuelCardsApi : ApiPointsBase
    {
        #region
        private readonly IMapper mapper;

        public FuelCardsApi(
            IHttpService httpService,
            IOptions<GateWayConfigurations> options,
            IMapper mapper)
            : base(httpService, options)
        {
            this.mapper = mapper;
        }

        public override ControllersNames ControllerName => ControllersNames.fuelcards;
        #endregion

        #region GET
        /// <summary>
        /// Метод для получения коллекции топливных карт
        /// </summary>
        /// <returns>Коллекция топливных карт</returns>
        public async Task<List<FuelCardFullModel>> GetCards(CancellationToken token)
            => (await http?.SendRequestAsync<List<FuelCardFullModel>>(
                GetApiPoint(),
                HttpMethod.Get,
                token)) ?? new();

        public async Task<FuelCardModel> GetCard(int cardsId, CancellationToken token)
            => (await http?.SendRequestAsync<FuelCardModel>(
                GetApiPoint(cardsId.ToString()),
                HttpMethod.Get,
                token)) ?? new();

        /// <summary>
        /// Метод для получения из БД полной коллекции отчетов водителя
        /// </summary>
        /// <returns>Коллекция отчетов водителя</returns>
        public async Task<List<FuelCardNotFoundView>> GetCardsNotFound(CancellationToken token)
            => (await http?.SendRequestAsync<List<FuelCardNotFoundView>>(
                GetApiPoint("notfoundcards"),
                HttpMethod.Get,
                token)) ?? new();

        /// <summary>
        /// Метод для получения коллекции альтернативных номеров топл.карты
        /// </summary>
        /// <returns>Коллекция  альтернативных номеров топл.карт</returns>
        public async Task<List<FuelCardsAlternativeNumberRequest>> GetCardsAlternativeNumbers(int cardId, CancellationToken token)
            => (await http?.SendRequestAsync<List<FuelCardsAlternativeNumberRequest>>(
                GetApiPoint($"alternativenumbers/{cardId}"),
                HttpMethod.Get,
                token: token)) ?? new();
        #endregion

        #region PUT
        /// <summary>
        /// Метод для сохранения в БД отредактированной топливной карты
        /// </summary>
        /// <returns>Отредактированная топливная карта</returns>
        public async Task<ResponseSingleAction<FuelCardShortModel>> PutCard(RequestSingleAction<FuelCardModel> card, CancellationToken token)
            => (await http?.SendRequestAsync<RequestSingleAction<FuelCardModel>, ResponseSingleAction<FuelCardShortModel>>(
                GetApiPoint(),
                HttpMethod.Put,
                card,
                token)) ?? new();

        /// <summary>
        /// Метод для отправкии коллекции карт в архив - путем установки флага IsArchived в положение true
        /// </summary>
        /// <param name="cards">Коллекция карт</param>
        /// <returns>Резальтат группового добавления топливных карт в архив</returns>
        public async Task<ResponseGroupActionDetailed<FuelCardShortModel, FuelCardModel>> PutCards(RequestGroupAction<FuelCardModel> cards, CancellationToken token)
        {
            return (await http?.SendRequestAsync<RequestGroupAction<FuelCardModel>, ResponseGroupActionDetailed<FuelCardShortModel, FuelCardModel>>(
                GetApiPoint("cards"),
                HttpMethod.Put,
                cards,
                token)) ?? new();
        }

        public async Task<ResponseSingleAction<FuelCardsAlternativeNumberRequest>> PutCardsAlternativeNumbers(
            FuelCardsAlternativeNumberRequest alterNum, CancellationToken token)
            => (await http?.SendRequestAsync<FuelCardsAlternativeNumberRequest, ResponseSingleAction<FuelCardsAlternativeNumberRequest>>(
                GetApiPoint("alternativenumber"),
                HttpMethod.Put,
                alterNum,
                token)) ?? new();
        #endregion

        #region POST
        /// <summary>
        /// Метод для группового добавления топливных карт в БД
        /// </summary>
        /// <param name="cards">Коллекция топливных карт, подлежащая добавлению в БД</param>
        /// <returns>Результат операции</returns>
        public async Task<ResponseGroupActionDetailed<FuelCardShortModel, FuelCardModel>> PostCards(
            RequestGroupAction<FuelCardModel> cards, CancellationToken token)
            => (await http?.SendRequestAsync<RequestGroupAction<FuelCardModel>, ResponseGroupActionDetailed<FuelCardShortModel, FuelCardModel>>(
                GetApiPoint(),
                HttpMethod.Post,
                cards,
                token)) ?? new();

        public async Task<ResponseSingleAction<FuelCardsAlternativeNumberRequest>> PostCardsAlternativeNumber(
            FuelCardsAlternativeNumberRequest alterNum, 
            CancellationToken token)
            => (await http?.SendRequestAsync<FuelCardsAlternativeNumberRequest, ResponseSingleAction<FuelCardsAlternativeNumberRequest>>(
                GetApiPoint("alternativenumber"),
                HttpMethod.Post,
                alterNum,
                token)) ?? new();
        #endregion

        // DELETE
        #region
        public async Task<ResponseGroupActionDetailed<FuelCardShortModel, FuelCardModel>> DeleteCards(List<FuelCardShortModel> cards, CancellationToken token)
            => (await http?.SendRequestAsync<List<FuelCardShortModel>, ResponseGroupActionDetailed<FuelCardShortModel, FuelCardModel>>(
                GetApiPoint("delete"),
                HttpMethod.Post,
                cards,
                token)) ?? new();

        public async Task<ResponseBase> DeleteCardsAlternativeNumbers(int alterNumId, CancellationToken token)
            => (await http?.SendRequestAsync<ResponseBase>(
                GetApiPoint($"alternativenumbers/{alterNumId}"),
                HttpMethod.Delete,
                token)) ?? new();
        #endregion
    }
}
