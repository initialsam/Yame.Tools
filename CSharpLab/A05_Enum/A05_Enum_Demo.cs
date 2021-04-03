using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A05_Enum
{
    public class A06_Enum_Demo
    {
        public void Demo()
        {
            var day = Week.周一 | Week.周二; //加入  周一,周二
            day |= Week.周三; //加入 周三
            day &= ~Week.周四;//移除 周四
            var a = day.HasFlag(Week.周一); //false
            var b = day.HasFlag(Week.周日); //true

            day = Week.工作日;
            var c = day.HasFlag(Week.周一 | Week.周四 | Week.周五); //true
            var d = day.HasFlag(Week.周一 | Week.周日); //false

            //使用擴充方法後
            day = Week.假日;
            day = day.AddFlag(Week.周四 | Week.周五);
            day = day.AddFlag(Week.周一);
            day = day.RemoveFlag(Week.周四);
            day = day.RemoveFlag(Week.周一 | Week.周五);
            var v = EnumFlagExtension.GetValues<Week>();
        }
    }

    public static class EnumFlagExtension
    {
        /// <summary>
        /// Adds a flag value to enum.
        /// Please note that enums are value types so you need to handle the RETURNED value from this method.
        /// Example: myEnumVariable = myEnumVariable.AddFlag(CustomEnumType.Value1);
        /// </summary>
        public static T AddFlag<T>(this Enum type, T enumFlag)
        {
            try
            {
                return (T)(object)((int)(object)type | (int)(object)enumFlag);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Could not append flag value {0} to enum {1}", enumFlag, typeof(T).Name), ex);
            }
        }

        /// <summary>
        /// Removes the flag value from enum.
        /// Please note that enums are value types so you need to handle the RETURNED value from this method.
        /// Example: myEnumVariable = myEnumVariable.RemoveFlag(CustomEnumType.Value1);
        /// </summary>
        public static T RemoveFlag<T>(this Enum type, T enumFlag)
        {
            try
            {
                return (T)(object)((int)(object)type & ~(int)(object)enumFlag);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Could not remove flag value {0} from enum {1}", enumFlag, typeof(T).Name), ex);
            }
        }
        /// <summary>
        /// Present the enum values as a comma separated string.
        /// </summary>
        public static string GetValues<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException();
            var values = Enum.GetNames(typeof(T));
            return string.Join(", ", values);
        }
    }
}
