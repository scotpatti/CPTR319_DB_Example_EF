using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class TimeSlot
    {
        public string TimeSlotId { get; set; } = null!;
        public string Day { get; set; } = null!;
        public decimal StartHr { get; set; }
        public decimal StartMin { get; set; }
        public decimal? EndHr { get; set; }
        public decimal? EndMin { get; set; }
    }
}
