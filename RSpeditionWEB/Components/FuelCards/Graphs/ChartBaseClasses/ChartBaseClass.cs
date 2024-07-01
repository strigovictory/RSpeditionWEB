namespace RSpeditionWEB.Components.FuelCards.Graphs.ChartBaseClasses
{
    public class ChartBaseClass<T> : ComponentBaseClass
    {
        // Коллекция-словарь наименование серии - коллекция значений внутри серии
        protected Dictionary<string, List<T>> data;
        protected Dictionary<string, List<T>> Data
        {
            get => this.data;
            set
            {
                if (value is not null)
                    this.data = value;
                else
                    this.data = new();

                this.InitColorsForChartsSeries();
            }
        }

        // Наименование графика
        [Parameter]
        public string TitleChart { get; set; }

        // Наименование категории, по которой будет идти группировка
        [Parameter]
        public string CategoryProperty { get; set; }

        // Наименование оси категории
        [Parameter]
        public string CategoryLabel { get; set; }

        // Максимальное значение для оси категории
        [Parameter]
        public object CategoryMax { get; set; }

        // Минимальное значение для оси категории
        [Parameter]
        public object CategoryMin { get; set; }

        // Минимальное значение для оси категории
        [Parameter]
        public object CategoryStep { get; set; }

        // Формат отображения для оси категории
        [Parameter]
        public Func<object, string> CategoryFormatter { get; set; }

        // Наименование оси значений, значения которой будут отражаться
        [Parameter]
        public string ValueProperty { get; set; }

        // Наименование оси значений
        [Parameter]
        public string ValueLabel { get; set; }

        // Минимальное значение для оси значений
        [Parameter]
        public object ValueMin { get; set; }

        // Максимальное значение для оси значений
        [Parameter]
        public object ValueMax { get; set; }

        // Минимальное значение для оси значений
        [Parameter]
        public object ValueStep { get; set; }

        // Формат отображения для оси значений
        [Parameter]
        public string ValueFormatString { get; set; }

        // Цвет основного заполнителя
        [Parameter]
        public string Fill { get; set; }

        // Цвета для серий
        [Parameter]
        public List<string> ColorsToFill { get; set; }

        [Parameter]
        public List<string> ColorsToStroke { get; set; }

        // Тип линии
        [Parameter]
        public LineType Line { get; set; }

        // Единица измерения 
        [Parameter]
        public string ValueUnit { get; set; }

        [CascadingParameter]
        public IStateItemsWereChanged<Dictionary<string, List<T>>> IStateItemsWereChangedCascadingVal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            // Проинициализировать коллекцию с данными
            this.Data = this.IStateItemsWereChangedCascadingVal?.Items ??  new();
            this.IsRender = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
                // Подписаться на событие родительского компонента, если данные в коллекции Data изменились
                this.IStateItemsWereChangedCascadingVal.StateItemsWereChangedEvent += this.ReactOnEventInParentComponent;
        }

        protected void ReactOnEventInParentComponent(Dictionary<string, List<T>> data)
        {
            this.Data = data;
            this.StateHasChanged();
        }

        /// <summary>
        /// Метод для инициализации коллекции цветов
        /// </summary>
        private void InitColorsForChartsSeries()
        {
            this.ColorsToFill = new();
            this.ColorsToStroke = new();

            var allNames = typeof(ColorsFill).GetEnumNames();
            var ind = -1;

            for (var i = 0; i < this.Data?.Keys?.Count(); i++)
            {
                ind++;
                var color = string.Empty;
                if(ind < allNames.Count())
                {
                    color = allNames.ElementAt(ind);
                }
                else
                {
                    ind = 0;
                    color = allNames.ElementAt(ind);
                }

                //var val = string.Concat("#", color);
                this.ColorsToFill.Add(color);
                this.ColorsToStroke.Add(color);
            }
        }
    }
}
