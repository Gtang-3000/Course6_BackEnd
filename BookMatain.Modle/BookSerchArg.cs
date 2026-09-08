using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Modle
{
    public class BookSerchArg
    {
        public string? BookName { get; set; }
        public string? BookClassId { get; set; }
        public string? BookKeeper { get; set; }
        public string? BookStatus { get; set; }

        public string? BookStartTime { get; set; }

        public string? BookEndTime { get; set; }
    }
}
