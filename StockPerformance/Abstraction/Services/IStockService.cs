using Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Abstraction.Services
{
    public interface IStockService
    {
        public Task<AggregatesRangeDto> GetAggregateBarsOverLastWeekAsync(string stocksTicker);
        public Task<List<List<string>>> GetAggregateBarsOverLastWeekWithComparisonAsync(string stocksTicker);
        public Task<AggregatesRangeDto> GetAggregateBarsOverDayAsync(string stocksTicker, DateTime dateTime);
        public Task<List<List<string>>> GetAggregateBarsOverDayWithComparisonAsync(string stocksTicker, DateTime dateTime);
    }
}
