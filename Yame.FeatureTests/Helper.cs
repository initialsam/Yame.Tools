using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests
{
    public static class Helper
    {
        public static  (string firstName, string lastName) SplitName(string fullName)
        {
            string[] vals = fullName.Split(' ');
            return (vals[0], vals[1]);
        }
    }
}
