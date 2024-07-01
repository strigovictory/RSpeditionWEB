using RSpeditionWEB.Configs;

namespace RSpeditionWEB.API
{
    public class AdminApi : ApiPointsBase
    {
        public AdminApi(
            IHttpService httpService,
            IOptions<GateWayConfigurations> options)
            : base(httpService, options)
        {
        }

        public override ControllersNames ControllerName => ControllersNames.admin;

        // GET
        #region
        public async Task<List<ModuleContainerView>> GetModuleContainers(CancellationToken token) 
            => await http?.SendRequestAsync<List<ModuleContainerView>>(
                GetApiPoint(nameof(GetModuleContainers)),
                HttpMethod.Get,
                token) ?? new();

        public async Task<List<TableSettingView>> GetTableSettings(CancellationToken token)
            => await http?.SendRequestAsync<List<TableSettingView>>(
                GetApiPoint(nameof(GetTableSettings)), 
                HttpMethod.Get,
                token) ?? new();

        /// <summary>
        /// Метод для получения коллекции типов классов из заданной сборки, к которым применен табличный атрибут
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <returns>Коллекция типов классов</returns>
        public List<Type> GetAllTypesWithTableAttributes(Assembly assembly) 
            => assembly?.GetTypes()?
            .Where(_ => _.IsClass && _.IsVisible && !_.IsAbstract)?
            .Where(_ => _.GetCustomAttribute(typeof(TableAttribute)) != null)?.ToList() ?? new();

        /// <summary>
        /// Метод для получения коллекции типов классов из заданной сборки, к которым применен табличный атрибут, значение схемы которого равно заданному
        /// </summary>
        /// <param name="schema">Схема</param>
        /// <param name="assembly">Сборка</param>
        /// <returns>Коллекция типов классов</returns>
        public List<Type> GetAllTypesWithTableAttr(Assembly assembly, string schema) 
            => assembly?.GetTypes()?
            .Where(_ => _.IsClass && _.IsVisible && !_.IsAbstract)?
            .Where(_ => _.GetCustomAttribute(typeof(TableAttribute)) != null)?
            .Where(_ => ((_.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute)?.Schema ?? string.Empty).Equals(schema))?.ToList() ?? new();

        /// <summary>
        /// Метод для получения коллекции настроек столбцов заданной таблицы в заданной схеме
        /// </summary>
        /// <param name="schemaName">Наименование схемы</param>
        /// <param name="tableName">Наименование таблицы</param>
        /// <returns></returns>
        public TableSettingView GetTableSetting(SchemaName schemaName, string tableName)
        {
            TableSettingView res = new();

            // Найти тип модели/класса с заданной схемой и наименованием таблицы в БД
            var foundTable = GetAllTypesWithTableAttributes(
                Assembly.GetAssembly(typeof(FuelTransactionFullResponse)))?.FirstOrDefault(
                    _ => IsConditionCompleted(_?.GetCustomAttribute(typeof(TableAttribute)), schemaName, tableName));

            return res;
        }

        /// <summary>
        /// Метод для получения настроек заданной таблицы в заданной схеме
        /// </summary>
        /// <param name="schemaName">Наименование схемы</param>
        /// <param name="tableName">Наименование таблицы</param>
        /// <returns>Настройки таблицы</returns>
        public async Task<TableSettingView> GetTableSetting(SchemaName schemaName, string tableName, CancellationToken token)
            => (await GetTableSettings(token))?.FirstOrDefault(_ => IsConditionCompleted(_, schemaName, tableName));

        /// <summary>
        ///  Метод для получения коллекции схем таблиц в БД
        /// </summary>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Коллекция схем таблиц в БД</returns>
        public async Task<List<string>> GetSchemaNames(CancellationToken token)
            => await http?.SendRequestAsync<List<string>>(
                GetApiPoint(nameof(GetSchemaNames)),
                HttpMethod.Get,
                token) ?? new();

        /// <summary>
        /// Метод для получения типа класса по его наименованию
        /// </summary>
        /// <param name="className">Имя класса</param>
        /// <returns>Тип</returns>
        public Type GetTypeByClassName(string className)
            => Assembly.GetAssembly(typeof(TableSettingView))?.GetTypes()?.FirstOrDefault(_ => _.Name.Equals(className, StringComparison.InvariantCulture));

        #endregion

        // POST
        #region
        /// <summary>
        /// Метод для добавления в бд нового модуля-контейнера для табличных настроек
        /// </summary>
        /// <param name="item">Вьюмодель добавляемого модуля-контейнера</param>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Результат операции</returns>
        public async Task<ResponseSingleAction<ModuleContainerView>> CreateModuleContainer(ModuleContainerView item, CancellationToken token)
            => await http?.SendRequestAsync<ModuleContainerView, ResponseSingleAction<ModuleContainerView>>(
                GetApiPoint(nameof(CreateModuleContainer)),
                HttpMethod.Post,
                item,
                token) ?? new();

        /// <summary>
        /// Метод для добавления в бд новых настроек выбранной таблицы
        /// </summary>
        /// <param name="item">Вьюмодель добавляемых настроек таблицы</param>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Результат операции</returns>
        public async Task<ResponseSingleAction<TableSettingView>> CreateTableSetting(TableSettingView item, CancellationToken token)
            => await http?.SendRequestAsync<TableSettingView, ResponseSingleAction<TableSettingView>>(
                GetApiPoint(nameof(CreateTableSetting)),
                HttpMethod.Post,
                item,
                token) ?? new();
        #endregion

        // PUT
        #region
        /// <summary>
        /// Метод для обновления в бд модуля-контейнера для табличных настроек
        /// </summary>
        /// <param name="item">Вьюмодель модуля-контейнера</param>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Результат операции</returns>
        public async Task<ResponseSingleAction<ModuleContainerView>> UpdateModuleContainer(ModuleContainerView item, CancellationToken token)
            => await http?.SendRequestAsync<ModuleContainerView, ResponseSingleAction<ModuleContainerView>>(
                GetApiPoint(nameof(UpdateModuleContainer)),
                HttpMethod.Put,
                item,
                token) ?? new();

        /// <summary>
        /// Метод для обновления в бд настроек выбранной таблицы
        /// </summary>
        /// <param name="item">Вьюмодель настроек таблицы</param>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Результат операции</returns>
        public async Task<ResponseSingleAction<TableSettingView>> UpdateTableSetting(TableSettingView item, CancellationToken token)
            => await http?.SendRequestAsync<TableSettingView, ResponseSingleAction<TableSettingView>>(
                GetApiPoint(nameof(UpdateTableSetting)),
                HttpMethod.Put,
                item,
                token) ?? new();
        #endregion

        // DELETE
        #region
        /// <summary>
        /// Метод для удаления из бд модуля-контейнера и связанных с ним табличных настроек
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля-контейнера</param>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Результат операции</returns>
        public async Task<ResponseBase> DeleteModuleContainer(int moduleId, CancellationToken token)
            => await http?.SendRequestAsync<int, ResponseBase>(
                GetApiPoint(nameof(DeleteModuleContainer)),
                HttpMethod.Delete,
                moduleId,
                token) ?? new();

        /// <summary>
        /// Метод для удаления из бд табличных настроек
        /// </summary>
        /// <param name="tsId">Идентификатор табличных настроек</param>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Результат операции</returns>
        public async Task<ResponseBase> DeleteTableSetting(int tsId, CancellationToken token)
            => await http?.SendRequestAsync<int, ResponseBase>(
                GetApiPoint(nameof(DeleteTableSetting)),
                HttpMethod.Delete,
                tsId,
                token) ?? new();
        #endregion

        // Additional methods
        #region
        /// <summary>
        /// Вcпомогательный метод для проверки наличия в БД идентичных наименования модулей-контейнеров
        /// </summary>
        /// <param name="name">Наименование модуля-контейнера, проверка уникальности которого осуществляется в пределах метода</param>
        /// <param name="token">Токен отмены запроса</param>
        /// <returns>Истина - если не уникальное наименование, и наоборот</returns>
        private async Task<bool> IsModuleNameUnique(string name, CancellationToken token)
        {
            List<ModuleContainerView> allModules = new();
            try
            {
                allModules = await GetModuleContainers(token);
            }
            catch (Exception exc)
            {
                throw;
            }

            return !(allModules?.Any(_ => _.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? false);
        }

        /// <summary>
        /// Метод для определения, равны ли значения name и schema табличного атрибута TableAttribute заданным
        /// </summary>
        /// <param name="attr">Атрибут</param>
        /// <param name="schemaName">Наименование схемы таблицы</param>
        /// <param name="tableName">Наименование таблицы</param>
        /// <returns>Истина, если условие выполняется</returns>
        private bool IsConditionCompleted(Attribute attr, SchemaName schemaName, string tableName)
        {
            var tableAttr = attr is null ? null : attr as TableAttribute;

            return (tableAttr?.Schema ?? string.Empty) == Enum.GetName(schemaName)
                 && (tableAttr?.Name ?? string.Empty).Equals(tableName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Метод для определения, равны ли значения наименование в БД и схема настроек таблицы TableAttribute заданным
        /// </summary>
        /// <param name="tableSetting">Настройки таблицы</param>
        /// <returns>Истина, если условие выполняется</returns>
        private bool IsConditionCompleted(TableSettingView tableSetting, SchemaName schemaName, string tableName)
            => (tableSetting?.DBSchemaName ?? string.Empty) == Enum.GetName(schemaName)
            && (tableSetting?.DBName ?? string.Empty).Equals(tableName, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Метод для первоначального заполнения коллекции настроек столбцов заданной таблицы
        /// </summary>
        /// <param name="item">Настройки таблицы</param>
        /// <returns>Коллекция настроек столбцов</returns>
        public List<TablesColumn> InitialFillingTableColumns(TableSettingView item)
        {
            List<TablesColumn> res = new();
            Type typeTable = null;

            // Тип таблицы, настройки которой инициируются
            try
            {
                typeTable = typeof(TableSettingView).Assembly.GetTypes()?.ToList()?.FirstOrDefault(_ => _.Name.Equals(item?.ClassName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase));
                var createdItem = Activator.CreateInstance(typeTable);
            }
            catch (Exception exc)
            {
                Log.Error($"Исключительная ситуация при попытке создания активатором экземпляра выбранного класса «{item?.ClassName ?? string.Empty}». " +
                                  $"Класс: {nameof(AdminApi)}, метод: «{nameof(InitialFillingTableColumns)}»." +
                                  $"Подробности: {exc?.Message ?? string.Empty}. {exc?.InnerException?.Message ?? string.Empty}");
                throw;
            }

            // Все открытые свойства, у которых тип значимый или строковый (не коллекция и ссылочный тип)
            var props = typeTable?.GetPropertiesSomeType();

            if (props != null && props?.Count > 0)
            {
                for (var i = 0; i < props?.Count; i++)
                {
                    var prop = props.ElementAt(i);

                    res.Add(FillCollumn(i, typeTable, prop));
                }
            }

            return res;
        }

        /// <summary>
        /// Метод для первоначального заполнения коллекции настроек столбцов заданной таблицы
        /// </summary>
        /// <param name="typeItem">Тип табличных настроек</param>
        /// <param name="prop">Информация о свойстве, на основании которого заполняется настройка столбца</param>
        /// <returns>Заполненные настройки столбца</returns>
        private TablesColumn FillCollumn(int ind, Type typeItem, PropertyInfo prop)
        {
            TablesColumn column = new();

            // 1 - Заполнить наименование столбца для user interface
            column.DisplayName = typeItem.GetCustomDisplayValues(prop?.Name ?? string.Empty).Item1 ?? string.Empty;

            // 2 - Заполнить наименование столбца из бд
            column.DBName = prop?.Name ?? string.Empty;

            // 3 - Заполнить порядок следования столбца 
            column.SortOrder = ++ind;

            // 4 - Определить, является ли столбец внешним ключом
            // 4.1 - ВНАЧАЛЕ ПРОВЕРИТЬ ПРИМЕНЕН ЛИ АТРИБУТПЕРВИЧНОГО КЛЮЧА
            var keyAttributes = prop.GetCustomAttributes<KeyAttribute>();

            // Если это первичный ключ 
            if (keyAttributes != null && keyAttributes.Count() > 0)
            {
                // Заполнить тип FK 
                column.IsIdentity = true;
            }
            else
            {
                //TODO: - пока не реализовано 

                // Заполнить тип FK 
                column.FK_ClassName = null;

                // Заполнить набор имен свойств для FK 
                column.FK_PropNames = new();
            }

            // 5 - Определить, является ли столбец внешним ключом
            // 5.1 - ВНАЧАЛЕ ПРОВЕРИТЬ ПРИМЕНЕН ЛИ АТРИБУТ ВНЕШНЕГО КЛЮЧА
            var customAttrForeignKey = prop.GetCustomAttribute<CustomForeignAttribute>();

            // Если это внешний ключ (не важно обозначен он в /БД или нет)
            if (customAttrForeignKey != null && !string.IsNullOrEmpty(customAttrForeignKey.NamePropForeignClass))
            {
                // Получить тип класса, экземпляр которого является вложенным в текущий T классом и связан с ним внешним ключом
                var typeInnerClass = customAttrForeignKey.TypeForeignClass;

                // Получить наименование свойства вложенного класса, которое будет отображаться в табличной части
                var namePropfKSelected = customAttrForeignKey.NamePropForeignClass;

                // Получить имена всех свойств вложенного класса, у которых тип значимый или строковый (не коллекция и ссылочный тип) 
                var propsNames = typeInnerClass?
                                               .GetPropertiesSomeType()?
                                               .Select(_ => _.Name)?
                                               .ToList()
                                               ??
                                               new()
                                               ;

                // Заполнить тип FK 
                column.FK_ClassName = typeInnerClass?.Name ?? string.Empty;

                // Заполнить набор имен свойств для FK 
                column.FK_PropNames = propsNames;

                // Заполнить отмеченное имя свойства для FK 
                column.FK_PropNameSelected = namePropfKSelected;
            }
            // 5.2- ЗАТЕМ ЕСЛИ АТРИБУТ НЕ ПРИМЕНЕН - ПРОВЕРИТЬ ЯВЛЯЕТСЯ ЛИ СВОЙСТВО НАВИГАЦИОННЫМ  
            else
            {
                //TODO: - пока не реализовано 

                // Заполнить тип FK 
                column.FK_ClassName = null;

                // Заполнить набор имен свойств для FK 
                column.FK_PropNames = new();
            }

            return column;
        }
        #endregion
    }
}
