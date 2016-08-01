using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloWorld.Tools
{
    /// <summary>
    /// http相关的扩展方法
    /// </summary>
    public static class HttpExtensions
    {

        /// <summary>
        /// 取得参数字典,字典的key已转为小写
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetParameters(this ActionExecutedContext context)
        {
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            if (context != null && context.HttpContext != null && context.HttpContext.Request != null)
            {
                setDictionary(dicData, context.HttpContext.Request);
                
            }
            return dicData;
        }

        private static void setDictionary(Dictionary<string, string> dicData, HttpRequest request)
        {
            if (request.Method.ToLower() == "get")
            {
                foreach (var item in request.Query)
                {
                    if (dicData.ContainsKey(item.Key.ToLower()))
                    {
                        dicData[item.Key.ToLower()] = dicData[item.Key] + "," + item.Value;
                    }
                    else
                        dicData.Add(item.Key.ToLower(), item.Value.ToString().SafeSqlString());
                }
            }
            else
            {
                foreach (var item in request.Form)
                {

                    if (dicData.ContainsKey(item.Key.ToLower()))
                    {
                        dicData[item.Key.ToLower()] = dicData[item.Key] + "," + item.Value;
                    }
                    else
                        dicData.Add(item.Key.ToLower(), item.Value.ToString().SafeSqlString());
                }
            }
        }

        /// <summary>
        /// 取得参数字典,字典的key已转为小写
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetParameters(this ActionExecutingContext context)
        {
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            if (context != null && context.HttpContext != null && context.HttpContext.Request != null)
            {
                setDictionary(dicData, context.HttpContext.Request);
            }
            return dicData;
        }

    }
}
