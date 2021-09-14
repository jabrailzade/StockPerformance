using Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace WebApi.Extensions
{
    public static class SymbolAggregateExtensions
    {
        public static List<string> CalculatePerformance(this List<SymbolAggregate> symbolAggregates)
        {
            var result = new List<string>();
            if (symbolAggregates == null || symbolAggregates.Count == 0)
            {
                return null;
            }

            var baseValue = symbolAggregates[0].ClosePrice;
            for (int i = 0; i < symbolAggregates.Count; i++)
            {
                var currentValue = symbolAggregates[i].ClosePrice;
                result.Add($"{(Math.Round((currentValue - baseValue) * 100 / currentValue, 2))}%");
            }
            
            return result;
        }
    }
}
