namespace Contracts.Dtos
{
    public class AggregatesRangeDto
    {
        public bool adjusted { get; set; }
        public int queryCount { get; set; }
        public string request_id { get; set; }
        public AggregateBarDto[] results { get; set; }
        public int resultsCount { get; set; }
        public string status { get; set; }
        public string ticker { get; set; }
    }
}
