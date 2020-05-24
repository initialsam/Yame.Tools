using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Entities
{
    public class LocationTag
    {
        public int LocationTagId { get; set; }

        public int Sequence { get; set; }

        public string LocationName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
