using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class Section
    {
        public Section()
        {
            Takes = new HashSet<Take>();
            Teaches = new HashSet<Teach>();
        }

        public string CourseId { get; set; } = null!;
        public string SecId { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public decimal Year { get; set; }
        public string? Building { get; set; }
        public string? RoomNumber { get; set; }
        public string? TimeSlotId { get; set; }

        public virtual Classroom? Classroom { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Take> Takes { get; set; }
        public virtual ICollection<Teach> Teaches { get; set; }
    }
}
