using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloWorld.Tools
{
    /// <summary>
    /// 通用的扩展方法
    /// </summary>
    public static class CommonExtensions
    {
        private static Regex _sqlkeywordregex = new Regex(@"(select|insert|delete|from|count\(|drop|table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec|master|net|local|group|administrators|user|or|and|--|;|,|\(|\)|\[|\]|\{|\}|%|@|\*|!|\')", RegexOptions.IgnoreCase);

        /// <summary>
        /// 替换字符串潜在注入的sql关键字
        /// </summary>
        /// <param name="str">需要替换的字符串</param>
        /// <param name="replace">替换的字符串</param>
        /// <returns></returns>
        public static string SafeSqlString(this string str, string replace = "")
        {

            if (str.IsNullOrEmpty())
            {
                return "";
            }
            MatchCollection matches = _sqlkeywordregex.Matches(str);
            for (int i = 0; i < matches.Count; i++)
                str = str.Replace(matches[i].Value, replace);
            return str;
        }

        /// <summary>
        /// 对象转int32
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns></returns>
        public static int ToInt32(this object str, int defaultValue = 0)
        {
            if (str.IsNullOrEmpty())
            {
                return defaultValue;
            }
            int result = defaultValue;
            Int32.TryParse(str.ToString(), out result);
            return result;
        }
        /// <summary>
        /// 对象转decimal
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object str, decimal defaultValue = 0)
        {
            if (str.IsNullOrEmpty())
            {
                return defaultValue;
            }
            decimal result = defaultValue;
            decimal.TryParse(str.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 对象是否为空
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object o)
        {
            if (o == null)
            {
                return true;
            }
            return o.ToString().IsNullOrEmpty();
        }
        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            if (s == null)
            {
                return true;
            }
            return string.IsNullOrEmpty(s);
        }
    }
}
