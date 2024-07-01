using fuelTranzGRPC;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RSpeditionWEB.Helpers;
using RSpeditionWEB.Proto;
using static fuelTranzGRPC.FuelTranzGRPCService;

namespace RSpeditionWEB.Services.GRPCServices
{
    public class FuelTranzGRPC : GRPCServiceBase<FuelTranzGRPCServiceClient>
    {
        public FuelTranzGRPC(FuelTranzGRPCServiceClient grpcClient) : base(grpcClient) { }

        public async Task<List<FuelTransactionFullResponse>> GetList_FuelTranz()
        {
            Serilog.Log.Information($"Start {nameof(FuelTranzGRPC)} {DateTime.Now.ToString("dd.MM.yyyyy HH:mm:ss")}");

            var res = await this.DoRequest<List<FuelTransactionFullResponse>>(methodName: nameof(FuelTranzGRPCServiceClient.GetListFuelTranz), methodsParameters: new List<dynamic> { new Google.Protobuf.WellKnownTypes.Empty() });

            return res;
        }

        protected override List<FuelTransactionFullResponse> MapResponse(object resp)
        {
            List<FuelTransactionFullResponse> res = new();

            Func<fuelTranzGRPC.FuelTranzReply, FuelTransactionFullResponse> mapper = (fuelTranzGRPC.FuelTranzReply item)
                => new FuelTransactionFullResponse
                {
                    Id = item.Id,
                    TransactionID = item.TransactionID,
                    OperationDate = item.OperationDate.ToDateTime(),
                    Quantity = item.Quantity.GetDecimalValue(),
                    Cost = item.Cost.GetDecimalValue(),
                    TotalCost = item.TotalCost.GetDecimalValue(),
                    IsCheck = item.IsCheck,
                    DivisionName = item.DivisionName,
                    ProviderId = item.FuelProviderId,
                    ProviderName = item.FuelProviderName,
                    FuelTypeId = item.FuelType,
                    FuelTypeName = item.FuelTypeName,
                    CurrencyId = item.CurrencyId,
                    CurrencyName = item.CurrencyName,
                    CardId = item.CardId,
                    CardName = item.CardName,
                    CountryId = item.CountryId,
                    CountryName = item.CountryName,
                    DriverReportId = item.DriverReportId
                };
            res = ForeachHelper<fuelTranzGRPC.FuelTranzReply, FuelTransactionFullResponse>.MapItemsList((resp as ListFuelTranzReply).Tranzactions?.ToList() ?? new(), mapper);
            Serilog.Log.Information($"Finish {nameof(FuelTranzGRPC)} {DateTime.Now.ToString("dd.MM.yyyyy HH:mm:ss")}");
            return res;
        }
    }
}
