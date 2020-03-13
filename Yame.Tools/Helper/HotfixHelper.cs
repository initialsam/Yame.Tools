using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yame.Tools.Helper
{
    public static class HotfixHelper
    {
        public static string GetHotfix(string content)
        {
            Regex rx = new Regex(@"KB[0-9]{6,7}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rx.Match(content).Value;
        }

    }
}
