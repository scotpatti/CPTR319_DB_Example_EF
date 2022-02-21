using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class StudentClass
    {
        public string Sid { get; set; } = null!;
        public string Sname { get; set; } = null!;
        public string? Major { get; set; }
        public decimal? TotCred { get; set; }
        public string CourseId { get; set; } = null!;
        public string SecId { get; set; } = null!;
        public string Semester { get; set; } = null!;
        public decimal Year { get; set; }
        public string? Grade { get; set; }
        public string? Building { get; set; }
        public string? RoomNumber { get; set; }
        public string? TimeSlotId { get; set; }
        public string Iid { get; set; } = null!;
        public string Iname { get; set; } = null!;
        public decimal? Coursecreds { get; set; }
        public string? Title { get; set; }
        public string? CourseDept { get; set; }
    }
}
