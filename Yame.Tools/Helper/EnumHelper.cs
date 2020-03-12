using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// 取得 Enum 的 Description
        /// </summary>
        /// <param name="value">Enum</param>
        /// <returns>Enum 的 Description</returns>
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute customAttribute = field.GetCustomAttribute<DescriptionAttribute>(false);

            if (customAttribute != null)
            {
                string name = string.IsNullOrWhiteSpace(customAttribute.Description) ? string.Empty : customAttribute.Description;
                if (!string.IsNullOrEmpty(name))
                    return name;
            }
            return value.ToString();
        }
        /// <summary>
        /// 取得 Enum 的 DisplayName
        /// </summary>
        /// <param name="value">Enum</param>
        /// <returns>Enum 的 DisplayName</returns>
        public static string GetEnumDisplayName(this Enum value)
        {

            FieldInfo field = value.GetType().GetField(value.ToString());

            DisplayAttribute customAttribute = field.GetCustomAttribute<DisplayAttribute>(false);
            if (customAttribute != null)
            {
                var resource = customAttribute.ResourceType;
                if (resource != null && !string.IsNullOrEmpty(customAttribute.Name))
                {
                    var rm = new ResourceManager(resource);
                    return rm.GetString(customAttribute.Name);
                }
                string name = string.IsNullOrWhiteSpace(customAttribute.GetName()) ? string.Empty : customAttribute.GetName();
                if (!string.IsNullOrEmpty(name))
                    return name;
            }
            return value.ToString();
        }

        public static string GetEnumDisplayMultiName(this Enum enumValue)
        {
            var enumMember = enumValue.GetType()
                            .GetMember(enumValue.ToString());

            DisplayAttribute displayAttrib = null;
            if (enumMember.Any())
            {
                displayAttrib = enumMember
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();
            }

            string name = null;
            Type resource = null;

            if (displayAttrib != null)
            {
                name = displayAttrib.Name;
                resource = displayAttrib.ResourceType;
            }

            return String.IsNullOrEmpty(name) ? enumValue.ToString()
                : resource == null ? name
                : new ResourceManager(resource).GetString(name);
        }

        public static List<T> ToList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList<T>();
        }

        public static IEnumerable<T> ToEnumerable<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
