using Azure;
using Azure.Core;
using Grpc.Core;
using Newtonsoft.Json.Linq;
using Plotly.Blazor;
using System.Linq.Dynamic.Core.Tokenizer;
using static fuelTranzGRPC.FuelTranzGRPCService;

namespace RSpeditionWEB.Services.GRPCServices
{
    public abstract class GRPCServiceBase<T> where T : ClientBase<T>
    {
        private readonly T gRPCClient;
        private CallOptions CallOptions { get; set; }
        private List<dynamic> MethodsParameters { get; set; }
        private MethodInfo MethodToInvoke { get; set; }

        public GRPCServiceBase(T gRPCClient)
        {
            this.gRPCClient = gRPCClient;
        }

        /// <summary>
        /// Метод вызова любого из методов, прописанных в proto-файле
        /// </summary>
        /// <param name="methodName">Наименование метода из файла .proto</param>
        /// <param name="methodsParameters">Параметры, передаваемые методу для его выполнения</param>
        /// <returns></returns>
        protected async Task<TResult> DoRequest<TResult>(string methodName, List<dynamic> methodsParameters)
        {
            object notMappedResp;
            try
            {
                this.MethodsParameters = methodsParameters;
                this.InitMethod(methodName);
                await this.AddMetadataToRequest();
                notMappedResp = this.MethodToInvoke?.Invoke(this.gRPCClient, this.MethodsParameters?.ToArray() ?? new object[] { }) ?? default;
            }
            catch (Exception exc)
            {
                Serilog.Log.Error($"Error in {this.GetType().Name}.{nameof(DoRequest)}. " +
                                  $"Details: {exc?.Message ?? string.Empty} {exc?.InnerException?.Message ?? string.Empty}");
                throw;
            }

            return this.MapResponse(notMappedResp);
        }

        private void InitMethod(string methodName)
        {
            var grpcType = this.gRPCClient?.GetType() ?? null;
            var methods = grpcType?.GetTypeInfo()?.GetDeclaredMethods(methodName) ?? null;
            this.MethodToInvoke = methods?.FirstOrDefault(_ => (_.GetParameters()?.Where(_ => !_.HasDefaultValue)?.Count() ?? 0) == (this.MethodsParameters?.Count() ?? 0) + 1);
        }

        private async Task AddMetadataToRequest()
        {
            //var headers = new Metadata();
            //var token = await this.TokenProvider?.GetWebToken();
            //headers.Add("Authorization", $"{TokenKind.WebAuthenticateToken.ToString()} {token}");
            this.CallOptions = new(headers: new());
            //this.CallOptions.Headers?.AddRange(headers);
            this.MethodsParameters = this.MethodsParameters?.Append(this.CallOptions)?.ToList() ?? new();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="resp">Динамический тип - прописан в proto-файле (например returns (ListFuelTranzReply))</param>
        /// <returns></returns>
        protected abstract dynamic MapResponse(object resp);
    }
}
