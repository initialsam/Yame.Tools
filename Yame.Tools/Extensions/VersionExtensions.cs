using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Extensions
{
    public static class VersionExtensions
    {
        /*
         Input.CompareTo(target)
        */
        /// <summary>
        /// 大於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool GreaterThan(this Version Input, Version target)
        {
            //return Input.CompareTo(target) == 1;
            return Input > target;
        }

        /// <summary>
        /// 小於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool LessThan(this Version Input, Version target)
        {
            //return Input.CompareTo(target) == -1;
            return Input < target;
        }

        /// <summary>
        /// 大於等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool GreaterThanOrEqual(this Version Input, Version target)
        {
            //return Input.CompareTo(target) >= 0;
            return Input >= target;
        }

        /// <summary>
        /// 小於等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool LessThanOrEqual(this Version Input, Version target)
        {
            //return Input.CompareTo(target) <= 0;
            return Input <= target;
        }

        /// <summary>
        /// 等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool Equal(this Version Input, Version target)
        {
            //return Input.CompareTo(target) == 0;
            return Input == target;
        }

        /// <summary>
        /// 不等於
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool NotEqual(this Version Input, Version target)
        {
            //return Input.CompareTo(target) != 0;
            return Input != target;
        }
    }
}
