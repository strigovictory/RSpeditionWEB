namespace RSpeditionWEB.Models.ViewModels.Cars
{
    public class TrailerView : IId
    {
        public int Id { get; set; }

        public string TrailerNumber { get; set; }

        public DateTime? OutputYear { get; set; }

        public virtual int TrailerTypeId { get; set; }

        public string TrailerTypeName { get; set; }

        public virtual int FkTrailerModelId { get; set; }

        public string TrailerModelName { get; set; }

        public virtual int? StatusId { get; set; }

        public string StatusName { get; set; }

        public override string ToString()
        => $"№ {this.TrailerNumber ?? string.Empty} ({this.TrailerTypeName ?? string.Empty}, {this.TrailerModelName ?? string.Empty})";
    }
}
