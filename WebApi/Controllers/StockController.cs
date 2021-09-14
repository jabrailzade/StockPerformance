using AutoMapper;
using Contracts;
using Contracts.Dtos;
using Core.Abstraction.Repositories;
using Core.Abstraction.Services;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private IStockService _stockService;
        private readonly IRepository<SymbolAggregate> _symbolAggregateRepo;
        private readonly IMapper _mapper;
        private const string _baseSymbol = "SPY";

        public StockController(IStockService stockService, IMapper mapper, IRepository<SymbolAggregate> symbolAggregateRepo)
        {
            _stockService = stockService;
            _mapper = mapper;
            _symbolAggregateRepo = symbolAggregateRepo;
        }

        // TODO: Method is just for data demonstration, need at least add filtering and pagination with max count limits for production
        [HttpGet("get-data")]
        public async Task<List<AggregateBarDto>> Get()
        {
            var result = await _symbolAggregateRepo.GetAll().ToListAsync();
            return _mapper.Map<List<AggregateBarDto>>(result);
        }

        [HttpGet("{stocksTicker}/range")]
        public async Task<List<List<string>>> GetAggregateBarsOverGivenDateRange(string stocksTicker)
        {
            return await _stockService.GetAggregateBarsOverLastWeekWithComparisonAsync(stocksTicker);
        }

        [HttpGet("{stocksTicker}/day")]
        public async Task<List<List<string>>> GetAggregateBarsOverDay(string stocksTicker, DateTime date)
        {
            return await _stockService.GetAggregateBarsOverDayWithComparisonAsync(stocksTicker, date);
        }
    }
}
