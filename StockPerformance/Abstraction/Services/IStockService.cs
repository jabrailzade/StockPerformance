using Contracts;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction.Services
{
    public interface IStockService
    {
        public Task<AggregatesRangeDto> GetAggregateBarsOverLastWeek(string stocksTicker, Timespan timespan);
    }
}
