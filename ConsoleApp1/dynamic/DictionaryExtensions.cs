﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    /// <summary>
    /// 字典类型拓展类
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 将对象转成字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input is IDictionary<string, object> dictionary)
                return dictionary;

            var properties = input.GetType().GetProperties();
            var fields = input.GetType().GetFields();
            var members = properties.Cast<MemberInfo>().Concat(fields.Cast<MemberInfo>());

            return members.ToDictionary(m => m.Name, m => GetValue(input, m));
        }

        /// <summary>
        /// 将对象转字典类型，其中值返回原始类型 Type 类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IDictionary<string, Tuple<Type, object>> ToDictionaryWithType(this object input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input is IDictionary<string, object> dictionary)
                return dictionary.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value == null ?
                        new Tuple<Type, object>(typeof(object), kvp.Value) :
                        new Tuple<Type, object>(kvp.Value.GetType(), kvp.Value)
                );

            var dict = new Dictionary<string, Tuple<Type, object>>();

            // 获取所有属性列表
            foreach (var property in input.GetType().GetProperties())
            {
                dict.Add(property.Name, new Tuple<Type, object>(property.PropertyType, property.GetValue(input, null)));
            }

            // 获取所有成员列表
            foreach (var field in input.GetType().GetFields())
            {
                dict.Add(field.Name, new Tuple<Type, object>(field.FieldType, field.GetValue(input)));
            }

            return dict;
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        private static object GetValue(object obj, MemberInfo member)
        {
            if (member is PropertyInfo info)
                return info.GetValue(obj, null);

            if (member is FieldInfo info1)
                return info1.GetValue(obj);

            throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
        }
    }
}

