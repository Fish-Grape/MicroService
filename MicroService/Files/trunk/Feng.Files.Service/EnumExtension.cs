using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Feng.Files.Service
{
    /// <summary>
    /// 更高效的获取枚举描述信息扩展类，参考http://www.cnblogs.com/anding/p/5129178.html
    /// </summary>
    public static class EnumExtension
    {
        private static ConcurrentDictionary<Enum, string> _ConcurrentDictionary = new ConcurrentDictionary<Enum, string>();
        /// <summary>
        /// 获取枚举的描述信息(Descripion)。
        /// 支持位域，如果是位域组合值，多个按分隔符组合。
        /// </summary>
        public static string GetDescription(this Enum @this)
        {
            return _ConcurrentDictionary.GetOrAdd(@this, (key) =>
            {
                var type = key.GetType();
                var field = type.GetField(key.ToString());
                //如果field为null则应该是组合位域值，
                return field == null ? key.GetDescriptions() : GetDescription(field);
            });
        }

        /// <summary>
        /// 获取位域枚举的描述，多个按分隔符组合
        /// </summary>
        public static string GetDescriptions(this Enum @this, string separator = ",")
        {
            var names = @this.ToString().Split(',');
            string[] res = new string[names.Length];
            var type = @this.GetType();
            for (int i = 0; i < names.Length; i++)
            {
                var field = type.GetField(names[i].Trim());
                if (field == null) continue;
                res[i] = GetDescription(field);
            }
            return string.Join(separator, res);
        }

        /// <summary>
        /// 获取枚举实体列表（值和描述）
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumModel> GetEnumList(Type enumType)
        {
            List<EnumModel> list = new List<EnumModel>();
            foreach (var item in Enum.GetValues(enumType))
            {
                EnumModel model = new EnumModel();
                object[] objArr = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    model.EnumDescription = da.Description;
                }
                model.EnumCode = Convert.ToInt32(item);
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 获取枚举实体列表（名称和描述）
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumNameModel> GetEnumNameList(Type enumType)
        {
            List<EnumNameModel> list = new List<EnumNameModel>();
            foreach (var item in Enum.GetValues(enumType))
            {
                EnumNameModel model = new EnumNameModel();
                object[] objArr = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    model.EnumDescription = da.Description;
                }
                model.EnumName = item.ToString();
                list.Add(model);
            }
            return list;
        }

        private static string GetDescription(FieldInfo field)
        {
            var att = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att == null ? field.Name : ((DescriptionAttribute)att).Description;
        }
    }
}
