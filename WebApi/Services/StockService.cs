using AutoMapper;
using Contracts.Dtos;
using Core.Abstraction.Repositories;
using Core.Abstraction.Services;
using Core.Domain.Entities;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ApiClients;
using WebApi.Extensions;

namespace WebApi.Services
{
    public class StockService : IStockService
    {
        private IStockApiClient _stockApiClient;
        private readonly IRepository<SymbolAggregate> _symbolAggregateRepo;
        private readonly IMapper _mapper;
        private const string _baseSymbol = "SPY";
        private const string _defaultultiplier = "1";

        public StockService(IStockApiClient stockApiClient, IRepository<SymbolAggregate> symbolAggregateRepo, IMapper mapper)
        {
            _stockApiClient = stockApiClient;
            _symbolAggregateRepo = symbolAggregateRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Queries the data for last week by provided stocksTicker
        /// </summary>
        /// <param name="stocksTicker">The symbol code</param>
        /// <returns>The aggregated data for the last week by the code</returns>
        public async Task<AggregatesRangeDto> GetAggregateBarsOverLastWeekAsync(string stocksTicker)
        {
            var currentMonday = getDateByDayOfWeek(DayOfWeek.Monday);
            var previousMonday = currentMonday.AddDays(-7).GetFormattedDateTimeString();
            var previousSunday = currentMonday.AddDays(-1).GetFormattedDateTimeString();

            return await _stockApiClient.GetAggregateBarsOverGivenDateRangeAsync(stocksTicker, _defaultultiplier, Timespan.Day.ToString().ToLower(), previousMonday, previousSunday);
        }

        /// <summary>
        /// Queries the data with additional comparison for last week by provided stocksTicker
        /// </summary>
        /// <param name="stocksTicker">The symbol code</param>
        /// <returns>The aggregated data with additional comparison for the last week by the code</returns>
        public async Task<List<List<string>>> GetAggregateBarsOverLastWeekWithComparisonAsync(string stocksTicker)
        {
            //Loading last week data for requested symbol
            var lastWeekAggregates = await GetAggregateBarsOverLastWeekAsync(stocksTicker);

            // Mapping data
            var symbolAggregates = _mapper.Map<List<SymbolAggregate>>(lastWeekAggregates.results);
            symbolAggregates.ForEach(s => s.SymbolName = lastWeekAggregates.ticker);

            //Storing data in database
            await _symbolAggregateRepo.AddBatchAsync(symbolAggregates);

            //Performance comparison
            var baseLastWeekAggregates = await GetAggregateBarsOverLastWeekAsync(_baseSymbol);
            var baseSymbolAggregates = _mapper.Map<List<SymbolAggregate>>(baseLastWeekAggregates.results);

            return new List<List<string>>
            {
                symbolAggregates.CalculatePerformance(),
                baseSymbolAggregates.CalculatePerformance()
            };
        }

        /// <summary>
        /// Queries the data for the day by provided stocksTicker
        /// </summary>
        /// <param name="stocksTicker">The symbol code</param>
        /// <returns>The aggregated data for the day by the code</returns>
        public async Task<AggregatesRangeDto> GetAggregateBarsOverDayAsync(string stocksTicker, DateTime dateTime)
        {
            var date = dateTime.GetFormattedDateTimeString();
            return await _stockApiClient.GetAggregateBarsOverGivenDateRangeAsync(stocksTicker, _defaultultiplier, Timespan.Hour.ToString().ToLower(), date, date);
        }

        /// <summary>
        /// Queries the hourly data with additional comparison for the day by provided stocksTicker
        /// </summary>
        /// <param name="stocksTicker">The symbol code</param>
        /// <returns>The aggregated hourly data with additional comparison for the day by the code</returns>
        public async Task<List<List<string>>> GetAggregateBarsOverDayWithComparisonAsync(string stocksTicker, DateTime dateTime)
        {
            //Loading last week data for requested symbol
            var dailyAggregates = await GetAggregateBarsOverDayAsync(stocksTicker, dateTime);

            // Mapping data
            var symbolAggregates = _mapper.Map<List<SymbolAggregate>>(dailyAggregates.results);
            symbolAggregates.ForEach(s => s.SymbolName = dailyAggregates.ticker);

            //Storing data in database
            await _symbolAggregateRepo.AddBatchAsync(symbolAggregates);

            //Performance comparison
            var baseDailyAggregates = await GetAggregateBarsOverDayAsync(_baseSymbol, dateTime);
            var baseSymbolAggregates = _mapper.Map<List<SymbolAggregate>>(baseDailyAggregates.results);

            return new List<List<string>>
            {
                symbolAggregates.CalculatePerformance(),
                baseSymbolAggregates.CalculatePerformance()
            };
        }

        /// <summary>
        /// Calculates and returns the nearest previous date by the dayOfWeek
        /// </summary>
        /// <param name="dayOfWeek">The day of week which date is needed to calculate</param>
        /// <returns>The nearest previous date by the dayOfWeek</returns>
        private DateTime getDateByDayOfWeek(DayOfWeek dayOfWeek)
        {
            DayOfWeek weekStart = dayOfWeek;
            DateTime startingDate = DateTime.Today;

            while (startingDate.DayOfWeek != weekStart)
                startingDate = startingDate.AddDays(-1);

            return startingDate;
        }
    }
}
