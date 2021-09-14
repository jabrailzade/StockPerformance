using System;

namespace Core.Domain.Entities
{
    public class SymbolAggregate : BaseEntity
    {
        public string SymbolName { get; set; }
        public float ClosePrice { get; set; }
        public float HighestPrice { get; set; }
        public float LowestPrice { get; set; }
        public long NumberOfTransactions { get; set; }
        public float OpenPrice { get; set; }
        public DateTime StartTime { get; set; }
    }
}
