using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class DailyOpenCloseDto
    {
        public float afterHours { get; set; }
        public float close { get; set; }
        public DateTime from { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float open { get; set; }
        public float preMarket { get; set; }
        public string status { get; set; }
        public string symbol { get; set; }
        public int volume { get; set; }
    }

}
