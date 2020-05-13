using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Yame.Tools.CustomAttribute
{
    /*
    [單元測試]0800自動截斷屬性值
    https://youtu.be/S947NenkvqA
    */
    public static class StringHelper
    {
        public static void AutoTrim<T>(T source) where T : class
        {
            if (source == null) return;

            var targetProperties = GetAutoTrimProperties(typeof(T));
            targetProperties.ForEach(x => TryPurgeString(x, source));
        }

        private static void TryPurgeString<T>(PropertyInfo prop,T source)
        {
            object oValue = prop.GetValue(source);
            if (oValue == null) return;

            if (prop.PropertyType != typeof(string)) return;

            string value = oValue.ToString();
            int maxLength = ((AutoTrimAttribute) prop.GetCustomAttributes(typeof(AutoTrimAttribute), true)[0])
                .MaxLength;
            if (value.Length <= maxLength) return;

            prop.SetValue(source, value.Substring(0, maxLength));
        }

        private static List<PropertyInfo> GetAutoTrimProperties(Type sourceType)
        {
            return sourceType.GetProperties()
                             .Where(x => x.GetCustomAttributes(typeof(AutoTrimAttribute), true).Length == 1)
                             .ToList();
        }
    }

    [AttributeUsage( AttributeTargets.Property,AllowMultiple =false)]
    public class AutoTrimAttribute : Attribute
    {
        public int MaxLength { get; }
        public AutoTrimAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}
