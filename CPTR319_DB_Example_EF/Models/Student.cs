using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class Student
    {
        public Student()
        {
            Takes = new HashSet<Take>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? DeptName { get; set; }
        public decimal? TotCred { get; set; }

        public virtual Department? DeptNameNavigation { get; set; }
        public virtual Advisor Advisor { get; set; } = null!;
        public virtual ICollection<Take> Takes { get; set; }
    }
}
