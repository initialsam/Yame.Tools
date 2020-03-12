using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class DirectionHelper
    {

        public static bool DefaultDesc(string direction)
        {
            return direction == "asc" ? false : true;
        }
        public static bool DefaultAsc(string direction)
        {
            return direction == "desc" ? true : false;
        }
    }
}
