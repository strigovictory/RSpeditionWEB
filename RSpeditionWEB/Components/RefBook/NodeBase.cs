using RSpeditionWEB.Models.ViewModels.DynamicNavigation;

namespace RSpeditionWEB.Components.RefBook
{
    public class NodeBase: ComponentBaseClass
    {
        private string nodeId;
        [Parameter]
        public string NodeId
        {
            get => this.nodeId;
            set
            {
                this.nodeId = value;
                this.InitMembers();
            }
        }

        public ModuleContainerView ModuleContainer { get; set; }

        public TableSettingView TableSetting { get; set; }

        // Флаг, показывающий - форма для редактирования или для создания новой модели
        // Если идентификатор не передан - значит форма для создания новой модели
        // и наоборот, если идентификатор передан - форма для редактирования существующей модели
        protected bool IsNew => (this.NodeId?.Trim()?.Length ?? 0) == 0 ? true : false;

        [Inject]
        protected AdminApi reflService { get; set; }

        // Коллекция модулей-контейнеров
        public List<ModuleContainerView> Modules { get; set; }

        // Коллекция табличных настроек
        protected List<TableSettingView> TableSettings { get; set; }

        // Если истина - то модель - модуль-контейнер, иначе - настройки таблицы
        protected bool IsModule { get; set; }
        protected bool IsChoiceDone { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await this.InitModules();
            await this.InitTables();
        }

        /// <summary>
        /// Метод для инициализации модулей-контейнеров
        /// </summary>
        private async Task InitModules() => this.Modules = (await this.reflService?.GetModuleContainers(this.Token) ?? new())?.OrderBy(_ => _.SortOrder)?.ToList() ?? new();

        /// <summary>
        /// Метод для инициализации модулей-контейнеров
        /// </summary>
        private async Task InitTables() => this.TableSettings = (await this.reflService?.GetTableSettings(this.Token) ?? new())?.OrderBy(_ => _.SortOrder)?.ToList() ?? new();

        /// <summary>
        /// Метод для получения модуля-контьейнера по идентификатору из параметров маршрута
        /// </summary>
        /// <returns>Найденный в БД модуль-контейнер</returns>
        protected ModuleContainerView GetModuleContainerById(string id)
        {
            ModuleContainerView res = new();
            var idStr = string.Empty;

            // Выделить из параметра маршрута сам идентификатор модуля без префикса
            if ((id?.IndexOf("m") ?? 0) != -1)
            {
                idStr = id?.Substring((id?.IndexOf("m") ?? 0) + 1) ?? string.Empty;
            }
            else
            {
                idStr = id;
            }

            if (Int32.TryParse(idStr, out int idConverted))
            {
                res = this.Modules?.FirstOrDefault(_ => _.Id == idConverted) ?? new();
            }

            return res;
        }

        /// <summary>
        /// Метод для получения табличных настроек по идентификатору из параметров маршрута
        /// </summary>
        /// <returns>Найденные в БД табличные настроки</returns>
        protected TableSettingView GetTableSettingById(string id)
        {
            TableSettingView res = new();

            if (Int32.TryParse(id, out int idConverted))
            {
                res = this.TableSettings?.FirstOrDefault(_ => _.Id == idConverted) ?? new();
            }

            return res;
        }

        /// <summary>
        /// Метод для инициализации членов компонента
        /// </summary>
        protected void InitMembers()
        {
            // Определить, узел это категория или подкатегория
            var indPrefix = this.NodeId?.IndexOf("m") ?? -1;
            if (indPrefix != -1)
            {
                this.IsModule = (this.NodeId?.Substring(indPrefix, 1) ?? string.Empty).Equals("m", StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                this.IsModule = false;
            }

            // Проинициализировать значение узла
            if (this.IsModule)
            {
                this.ModuleContainer = this.IsNew ? new() : this.GetModuleContainerById(this.NodeId);
                this.TableSetting = new();
            }
            else
            {
                this.ModuleContainer = new();
                this.TableSetting = this.IsNew ? new() : this.GetTableSettingById(this.NodeId);
            }

            // Обновить, чтобы в дочернем компоненте сработал метод  OnParametersSetAsync, который обновляет колекцию через каскадный параметр-ссылку на текущий компонент
            this.StateHasChanged();
        }
    }
}
