using Contracts;
using Core.Abstraction.Services;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiClients;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        //[HttpGet("{symbol}/{date}")]
        //public async Task<DailyOpenCloseDto> Get(string symbol, string date)
        //{
        //    return await _stockApiClient.GetDataBySymbolName(symbol, date);
        //}

        [HttpGet("{stocksTicker}/range/{timespan}")]
        public async Task<AggregatesRangeDto> GetAggregateBarsOverGivenDateRange(string stocksTicker, Timespan timespan)
        {
            return await _stockService.GetAggregateBarsOverLastWeek(stocksTicker, timespan);
        }
    }
}
