using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class Course
    {
        public Course()
        {
            Sections = new HashSet<Section>();
            Courses = new HashSet<Course>();
            Prereqs = new HashSet<Course>();
        }

        public string CourseId { get; set; } = null!;
        public string? Title { get; set; }
        public string? DeptName { get; set; }
        public decimal? Credits { get; set; }

        public virtual Department? DeptNameNavigation { get; set; }
        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Course> Prereqs { get; set; }
    }
}
