namespace RSpeditionWEB.Models.ViewModels.FuelCards
{
    public class NotParsedTransaction : FuelTransactionResponse
    {
        [JsonInclude]
        public string CardNumber { get; set; } // если номер топливной карты - самостоятельный идентификатор

        [JsonInclude]
        public string CarNumber { get; set; }// если номер топливной карты соответсвует номеру тягача

        [JsonInclude]
        public string NotFuelType { get; set; } // если услуга не относится к топливу - соответсвует наименованию услуги

        public override string ToString()
        {
            var res = FuelHelper.GetFuelTransactionString(this);

            if (!string.IsNullOrEmpty(this.CardNumber))
                res += $", топл.карта: {this.CardNumber}";

            if (!string.IsNullOrEmpty(this.CarNumber))
                res += $", тягач: {this.CarNumber}";

            if (!string.IsNullOrEmpty(this.NotFuelType))
                res += $", услуга: {this.NotFuelType}";

            return res ;
        }
    }
}
