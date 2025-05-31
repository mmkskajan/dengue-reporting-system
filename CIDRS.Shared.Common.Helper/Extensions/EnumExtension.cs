using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CIDRS.Shared.Common.Helper.Extensions
{
    /// <summary>
    /// The class that contains Enum Conversion method
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Method to get the description for the given enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        internal static string ToEnumString<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            var atts = type.GetDefaultMembers();
            // Tries to find a DescriptionAttribute for a potential friendly name for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                System.Collections.Generic.List<object> disAttrs = new System.Collections.Generic.List<object>();
                foreach (MemberInfo memberInfoItem in memberInfo)
                {
                    object[] attrs = memberInfoItem.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    disAttrs.AddRange(attrs);
                }

                //Pull out the description value
                if (disAttrs != null && disAttrs.Count > 0)
                    return ((DescriptionAttribute)disAttrs[0]).Description;
            }

            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        /// <summary>
        /// Method to get the enumeration for the given description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        internal static T FromEnumString<T>(this string description) where T : struct, IConvertible
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", "description");
        }
    }
}
