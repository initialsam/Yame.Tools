using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Entities
{
    public class ProjectInfo
    {
        public int ProjectInfoId { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsWishingPool { get; set; }

        [JsonIgnore()]
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

        [JsonIgnore()]
        public virtual ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();

        [JsonIgnore()]
        public virtual ICollection<UploadFile> Files { get; set; } = new List<UploadFile>();
    }
}
