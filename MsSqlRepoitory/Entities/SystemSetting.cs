using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory.Entities
{
    public class SystemSetting
    {
        public int Id { get; set; }
        public string UpdateNote { get; set; }
        public bool IsForceUpdateNote { get; set; }
    }
}
