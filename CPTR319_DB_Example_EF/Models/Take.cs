using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class Take
    {
        public string Id { get; set; } = null!;
        public string CourseId { get; set; } = null!;
        public string SecId { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public decimal Year { get; set; }
        public string? Grade { get; set; }

        public virtual Student IdNavigation { get; set; } = null!;
        public virtual Section Section { get; set; } = null!;
    }
}
