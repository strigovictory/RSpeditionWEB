using Plotly.Blazor;
using RSpeditionWEB.Configs;
using System.Threading.Tasks;

namespace RSpeditionWEB.Components.ComponentsBase
{
    public class ComponentBaseClass : AttrHelperClass, IDisposable
    {
        [CascadingParameter]
        public Error Error { get; set; }

        [Inject]
        protected IJSRuntime js { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public Serilog.ILogger logger { get; set; }

        [Inject]
        public IOptions<GateWayConfigurations> GateWayConfigs { get; set; }

        protected string HostUrl => (GateWayConfigs?.Value?.BaseUrl ?? string.Empty) + "/api/v1/";

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public int MinHeight { get; set; } = 70;

        [Parameter]
        public int MaxHeight { get; set; } = 75;

        protected string StyleMaxHeightName => $"max-height: {MinHeight}vh !important;";

        [Parameter]
        public int MaxHeightLastTableRow { get; set; } = 58;

        public string StyleMaxHeightLastTableRow => $"height: {MaxHeightLastTableRow}vh; cursor:auto;";

        [Parameter]
        public int? MaxHeightTableRow { get; set; } = null;

        public string StyleMaxHeightTableRow => MaxHeightTableRow.HasValue ? $"height: {MaxHeightLastTableRow}px;" : "";

        protected bool ShowMessage { get; set; }
        protected bool ShowError{ get; set; }

        protected bool IsRender { get; set; }

        [Parameter]
        public string ReturnUrl { get; set; } = "/";

        public bool NeedNavigateToReturnUrl { get; set; }
        public string Message { get; set; }
        protected bool ShowFilters { get; set; }
        protected string LabelOnButtonFilter => this.ShowFilters ? "Скрыть фильтры" : "Фильтры";

        // Обьект токена отмены
        private CancellationTokenSource TokenSource => new();

        // Токен отмены
        protected CancellationToken Token => this.TokenSource?.Token ?? default;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Отправка потребителям токена запроса на отмену.
            this.TokenSource?.Cancel();
            this.TokenSource?.Dispose();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.IdForProgressBar = $"{this?.GetType()?.Name ?? ""}_id";
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.TryGetValue<string>(nameof(ReturnUrl), out var value))
            {
                if (value is null)
                {
                    this.ReturnUrl = "/";
                }
            }

            await base.SetParametersAsync(parameters);
        }

        protected void RenderMessage()
        {
            this.ShowMessage = true;
            this.StateHasChanged();
        }

        protected void HideMessage()
        {
            this.ShowMessage = false;
            this.StateHasChanged();
        }

        /// Метод для формирования сообщения валидности заданного свойства у заданной модели
        /// </summary>
        /// <param name="model">Экземпляр класса валидность свойства которого проверяется</param>
        /// <param name="prop">Наименование свойства, валидность которого проверяется</param>
        /// <returns>Сообщение о валидности свойства модели</returns>
        protected string GetValidationMessageForProp(object model, string prop) => model.GetValidationMessageForProp(prop) ?? string.Empty;

        protected bool ShowModalProgressBar { get; set; }

        public string IdForProgressBar { get; set; }

        public async Task ProgressBarOpenAsync()
        {
            this.StartTimer();
            await this.js.InvokeVoidAsync("ShowBar", this.IdForProgressBar);
        }

        public async Task ProgressBarCloseAsync()
        {
            this.ResetTimer();
            await this.js.InvokeVoidAsync("CloseBar", this.IdForProgressBar);
        }

        protected virtual async Task ProcessingResponse(ResponseBase resp)
        {
            if (resp != null)
                await this.StartDataUpdating();
            else
            {
                this.Title = "Предупреждение";
                this.Message = $"{resp?.Result ?? "Операция закончилась с ошибкой ! Ошибка на уровне сервера !"}";
                this.RenderMessage();
            }
        }

        protected virtual async Task StartDataUpdating() => this.StateHasChanged();

        protected async Task IsRenderTrueAsync()
        {
            this.IsRender = true;
            await InvokeAsync(this.StateHasChanged);
        }
    }
}