namespace RSpeditionWEB.Components.Scaffolds.EditForm
{
    public abstract class ScaffoldJCDeleteGroupItemsBase<T>: FilteringTItemsBase<T> where T : class, ICloneable, new()
    {
        [CascadingParameter]
        public GroupActionFormBase<T> ParentComponent { get; set; }

        [Parameter]
        public EventCallback<List<T>> SendCheckedItemsToParent { get; set; }

        [Parameter]
        public string Label { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.ReturnUrl = this.ParentComponent?.ReturnUrl ?? "/";
            await this.InitButtonsAsync();
            this.IsRender = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            await this.InitButtonsAsync();
        }

        protected override async Task InitButtonsAsync()
        {
            this.ButtonBaseClassList = new();
            //
            ButtonBaseClass b1 = new();
            b1.CallbackToParent = (async () =>
            {
                this.ParentComponent.ShowConfermationDelete = false;
                await this.ParentComponent.StartOperation();
                if (this.ParentComponent?.OperationsResult == null)
                    this.Title = "Предупреждение";
                else
                    this.Title = "Результат операции";
                this.Message = this.ParentComponent?.OperationsResult?.Result ?? String.Empty; 
                this.RenderMessage();
            });
            b1.LabelForButton = $"Удалить отмеченные записи в колличестве {this.ParentComponent?.ItemsSelected.Count ?? 0} шт.";
            b1.IsActive = this.ParentComponent.IsActionAllowed;
            b1.TitleIfIsActive = "Кликните для удаления группы отмеченных записей";
            b1.TitleIfIsNonActive = "Для удаления д.б. отмечена хотя бы одна запись";
            //
            if (IsUserHasFuelRoleInHierarchy(Enums.Roles.FuelRoles.Fuel_Provisioner))
            {
                ButtonBaseClassList.AddRange(new List<ButtonBaseClass> { b1 });
            }
        }
    }
}
