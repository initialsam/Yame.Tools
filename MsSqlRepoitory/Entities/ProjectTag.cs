using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Entities
{
    public class ProjectTag
    {
        public int ProjectTagId { get; set; }
        public string ProjectTagName { get; set; }

        [JsonIgnore()]
        public virtual ICollection<ProjectInfo> ProjectInfos { get; set; }
    }
}
