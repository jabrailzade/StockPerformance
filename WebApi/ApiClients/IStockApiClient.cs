using System.Threading.Tasks;
using Contracts.Dtos;
using Refit;

namespace WebApi.ApiClients
{
    public interface IStockApiClient
    {
        [Get("/v2/aggs/ticker/{stocksTicker}/range/{multiplier}/{timespan}/{from}/{to}")]
        public Task<AggregatesRangeDto> GetAggregateBarsOverGivenDateRangeAsync(string stocksTicker, string multiplier, string timespan, string from, string to);
    }
}
