using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.FeatureTests.StructPerformance
{
    public struct WithRefWithOverride
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is WithRefWithOverride))
                return false;

            var other = (WithRefWithOverride)obj;

            return X == other.X &&
                   Y == other.Y &&
                   Description == other.Description;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        // GetHashCode override and == != operators omitted
    }
}
