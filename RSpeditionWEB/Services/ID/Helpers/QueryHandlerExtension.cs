using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSpeditionWEB.Services.ID.Helpers;

public static class QueryHandlerExtension
{
    /// <summary>
    /// Токен для авторизации в API. Имеет свойство меняться с течением времени (экспайрится, время - неизвестно. Неделю не эксп.)
    /// </summary>
    private static readonly string token = "791333C493A14F4C41ED7EB85FBFD2A7";

    public static async Task<RestResponse> ExecuteRequestQueryPost(
        RestClient applicationClient, 
        RestRequest outgoingRequest)
    {
        var response = await applicationClient.ExecutePostAsync(outgoingRequest);

        if (!response?.IsSuccessStatusCode ?? false)
        {
            Log.Error($"Code: {response?.StatusCode.ToString() ?? string.Empty} Message: {response?.ErrorMessage ?? string.Empty}");
        }

        return response;
    }

    public static async Task<T> ExecuteRequestQueryGet<T>(
        RestClient applicationClient,
        RestRequest outgoingRequest) where T : class
    {
        var response = await applicationClient.ExecuteGetAsync(outgoingRequest);

        if (!response?.IsSuccessStatusCode ?? false)
        {
            Log.Error($"Code: {response?.StatusCode.ToString() ?? string.Empty} Message: {response?.ErrorMessage ?? string.Empty}");
            return default;
        }

        return JsonConvert.DeserializeObject<T>(response.Content);
    }
}
