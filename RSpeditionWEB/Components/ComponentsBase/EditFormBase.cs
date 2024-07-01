using Microsoft.AspNetCore.Components.Forms;

namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class GroupEditFormBase<T> : EditFormBase<T> where T : class, ICloneable, new()
    {
        protected bool IsRenderSuccessItems { get; set; } = false;
        protected bool IsRenderSecondSuccessItems { get; set; } = false;
        protected bool IsRenderNotSuccessItems { get; set; } = false;

        protected List<string> AddedToDBItems { get; set; } = new();
        protected Dictionary<string, string> NotAddedToDBItems { get; set; } = new();

        protected virtual List<(int, string, string)> GetTupleAdded()
        {
            List<(int, string, string)> res = new();
            for (var i = 0; i < this.AddedToDBItems.Count; i++)
            {
                var item = this.AddedToDBItems[i];
                var name = item.ToString();
                res.Add((i+1, name, string.Empty));
            }

            return res;
        }

        protected virtual List<(int, string, string)> GetTupleNotAdded()
        {
            List<(int, string, string)> res = new();
            for (var i = 0; i < this.NotAddedToDBItems.Keys.Count; i++)
            {
                var item = this.NotAddedToDBItems.Keys.ElementAt(i);
                var reason = this.NotAddedToDBItems[item];
                res.Add((i+1, item, reason));
            }

            return res;
        }
    }
    public abstract class EditFormBase<T> : ActionForm<T> where T : class, ICloneable, new()
    {
        [Parameter]
        public T TItemFromParent { get; set; }

        // Контекст формы
        protected EditContext EditContext;

        private T model;
        public T Model
        {
            get => this.model;
            set
            {
                this.model = value;
                if(this.model != null)
                    this.EditContext = new(this.model);
            }
        }

        [Parameter]
        public OperationsType? OperationsType { get; set; }

        private bool IsIGuid => this.TItemFromParent != null && this.TItemFromParent is IGuid;

        protected Predicate<OperationsType?> IsCreatePredicate = (OperationsType? oper) => oper.HasValue && oper.Value == RSpeditionWEB.Enums.OperationsType.create;

        public virtual bool IsCreate => this.IsCreatePredicate(this.OperationsType);

        protected Type ModelsType => this.Model?.GetType();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await this.MedelsInitialization();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.TryGetValue<OperationsType?>(nameof(this.OperationsType), out var value))
            {
                if (value is null)
                    this.OperationsType = RSpeditionWEB.Enums.OperationsType.update; // если параметр не передается, то операция редактирования
                else
                    this.OperationsType = value;
            }

            await base.SetParametersAsync(parameters);
        }

        protected virtual async Task ResetValues() => await this.MedelsInitialization();

        protected virtual async Task MedelsInitialization()
        {
            if (this.IsCreate && !this.IsIGuid) // если тип наследует интерфейс IGuid, то создавать экземпляр не нужно - он уже создан и внутри конструктора заполнен guid
                this.Model = new();
            else
                this.Model = this.TItemFromParent != null ? this.TItemFromParent?.Clone() as T : new();
        }

        protected virtual (bool isValid, string errors) ValidateModel()
        {
            var check1 = this.Model?.CheckModelIsValid();

            return (
                        (check1.HasValue && check1.Value.isValid),
                        (check1.HasValue ? check1.Value.errors?.ConvertListToString('!') : "")
                   );
        }

        protected virtual bool IsModelValid => this.ValidateModel().isValid;

        protected string ValidErrors => this.ValidateModel().errors;

        protected string GetPropsValidMessage(string propsName) => this.Model.GetValidationMessageForProp(propsName ?? String.Empty);
    }
}
