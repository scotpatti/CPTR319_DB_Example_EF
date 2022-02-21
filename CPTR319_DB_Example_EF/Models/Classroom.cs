using System;
using System.Collections.Generic;

namespace CPTR319_DB_Example_EF.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            Sections = new HashSet<Section>();
        }

        public string Building { get; set; } = null!;
        public string RoomNumber { get; set; } = null!;
        public decimal? Capacity { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
