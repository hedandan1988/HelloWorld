using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorld.Tools;

namespace HelloWorld.FrontModel
{
    /// <summary>
    /// 查询模型对象
    /// </summary>
    public class SearchModel
    {
        public SearchModel(Dictionary<string, string> dictionary){
            Parameters = dictionary;
            InitParameters();
        }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } 
        /// <summary>
        /// 页码 从1开始
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 参数列表
        /// </summary>
        private Dictionary<string,string> Parameters { get; set; }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public string GetValue(string key,string defaultValue="")
        {
            if (key.Trim().IsNullOrEmpty())
            {
                return defaultValue;
            }
            if (Parameters.ContainsKey(key.Trim().ToLower()))
            {
                return Parameters[key];
            }
            return defaultValue;
        }
        /// <summary>
        /// 初始化查询模型
        /// </summary>
        /// <param name="dictionary">参数字典</param>
        public void InitParameters()
        {
            if (Parameters != null && Parameters.Count > 0)
            {
                if (Parameters.ContainsKey("pagesize"))
                {
                    PageSize = Parameters["pagesize"].ToInt32(10);
                    PageSize = PageSize > 0 ? PageSize : 10;
                }
                if (Parameters.ContainsKey("pageindex"))
                {
                    PageIndex = Parameters["pageindex"].ToInt32(1);
                    PageIndex = PageIndex > 0 ? PageIndex : 1;
                }
            }
        }
    }
}
