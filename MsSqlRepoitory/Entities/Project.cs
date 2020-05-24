using MsSqlRepoitory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        public int Sequence { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ServiceStartDate { get; set; }

        public DateTime? ServiceEndDate { get; set; }

        public int ProjectInfoId { get; set; }

        public virtual ProjectInfo ProjectInfo { get; set; }

        public int LocationTagId { get; set; }

        public virtual LocationTag LocationTag { get; set; }
       
    }
}
