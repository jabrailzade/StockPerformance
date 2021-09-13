using Contracts;
using Core.Abstraction.Services;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiClients;
using WebApi.Extensions;

namespace WebApi.Services
{
    public class StockService : IStockService
    {
        private IStockApiClient _stockApiClient;
        private const string _defaultultiplier = "1";

        public StockService(IStockApiClient stockApiClient)
        {
            _stockApiClient = stockApiClient;
        }

        public Task<AggregatesRangeDto> GetAggregateBarsOverLastWeek(string stocksTicker, Timespan timespan)
        {
            var currentMonday = getDayOfWeekDate(DayOfWeek.Monday);
            var previousMonday = currentMonday.AddDays(-7).GetFormattedDateTimeString();
            var previousSunday = currentMonday.AddDays(-1).GetFormattedDateTimeString();

            return _stockApiClient.GetAggregateBarsOverGivenDateRange(stocksTicker, _defaultultiplier, timespan.ToString().ToLower(), previousMonday, previousSunday);
        }

        public DateTime getDayOfWeekDate(DayOfWeek dayOfWeek)
        {
            DayOfWeek weekStart = dayOfWeek;
            DateTime startingDate = DateTime.Today;

            while (startingDate.DayOfWeek != weekStart)
                startingDate = startingDate.AddDays(-1);

            return startingDate;
        }
    }
}
