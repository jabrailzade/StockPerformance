using AutoMapper;
using Contracts.Dtos;
using Core.Domain.Entities;
using WebApi.Extensions;

namespace WebApi.MapperProfiles
{
    public class SymbolAggregateProfiles : Profile
    {
        public SymbolAggregateProfiles()
        {
            CreateMap<SymbolAggregate, AggregateBarDto>()
                .ForMember(a => a.StartTimestamp, a => a.MapFrom(s => s.StartTime.DateTimeToUnixTimestamp()));

            CreateMap<AggregateBarDto, SymbolAggregate>()
                .ForMember(s => s.StartTime, s => s.MapFrom(a => a.StartTimestamp.UnixTimeStampToDateTime()));
        }
    }
}
