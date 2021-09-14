using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SymbolAggregate> SymbolAggregates { get; set; }
    }
}
