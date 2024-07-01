using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RSpeditionWEB.Components.Upload;

namespace RSpeditionWEB.Models.ViewModels.FuelCards
{
    public class FuelReport : UploadedContent, ICloneable
    {
        public FuelReport() : base() { }

        [JsonInclude]
        [Required(ErrorMessage = "Поставщик топлива обязателен для заполнения")]
        [ValidateId(PropName = "Поставщик топлива")]
        public int ProviderId { get; set; }

        [JsonInclude]
        [Required(ErrorMessage = "Тип отчета обязателен для заполнения")]
        public bool? IsMonthly { get; set; }

        public override object Clone() => new FuelReport
        {
            ProviderId = this.ProviderId,
            IsMonthly = this.IsMonthly,
            FilePath = this.FilePath,
            Content = this.Content,
            FileName = this.FileName,
            UserName = this.UserName,
        };
    }
}
