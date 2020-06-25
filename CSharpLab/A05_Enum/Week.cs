using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A05_Enum
{
    [Flags]
    public enum Week
    {
        None = 0,
        周一 = 1,
        周二 = 2,
        周三 = 4,
        周四 = 8,
        周五 = 16,
        周六 = 32,
        周日 = 64,
        工作日 = 周一 | 周二 | 周三 | 周四 | 周五,
        假日 = 周六 | 周日,
        All = 工作日 | 假日
    }
}
