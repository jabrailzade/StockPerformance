using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Refit;

namespace WebApi.ApiClients
{
    public interface IStockApiClient
    {
        [Get("/v1/open-close/{stocksTicker}/{date}")]
        public Task<DailyOpenCloseDto> GetDataBySymbolName(string stocksTicker, string date);

        [Get("/v2/aggs/ticker/{stocksTicker}/range/{multiplier}/{timespan}/{from}/{to}")]
        public Task<AggregatesRangeDto> GetAggregateBarsOverGivenDateRange(string stocksTicker, string multiplier, string timespan, string from, string to);
    }
}
