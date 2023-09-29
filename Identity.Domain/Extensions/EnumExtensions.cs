using System;
using System.ComponentModel;

namespace Identity.Domain.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Provides the annotation property of the enum
        /// </summary>
        /// <param name="value">enumeration type</param>
        /// <returns>type of string</returns>
        /// <exception cref="ArgumentNullException">if method parameter null</exception>

        public static string GetEnumDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return value.ToString();

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attributes?.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
