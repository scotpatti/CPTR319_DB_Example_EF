using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class Advisor
    {
        public string SId { get; set; } = null!;
        public string? IId { get; set; }

        public virtual Instructor? IIdNavigation { get; set; }
        public virtual Student SIdNavigation { get; set; } = null!;
    }
}
