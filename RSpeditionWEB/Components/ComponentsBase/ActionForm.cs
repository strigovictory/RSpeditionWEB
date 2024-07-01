using RSpeditionWEB.Enums.Roles;

namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class ActionForm<T> : ObjItemsEventBase where T : class, ICloneable, new()
    {
        public virtual ResponseBase OperationsResult { get; set; }
        public virtual bool IsActionAllowed { get; }
        protected DateTime TimeStartProcess { get; set; }
        protected DateTime TimeFinishProcess { get; set; }
        protected int TimeProcessMin { get; set; }
        protected int TimeProcessSec { get; set; }

        public virtual async Task StartOperation() { }

        public virtual async Task ProccessingResponse() { }

        protected override FuelRoles MinimumFuelRole { get; set; } = FuelRoles.Fuel_Provisioner;
    }
}
