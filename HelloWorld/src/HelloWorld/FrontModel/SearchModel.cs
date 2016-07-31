using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.FrontModel
{
    /// <summary>
    /// 查询模型对象
    /// </summary>
    public class SearchModel
    {
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
        public Dictionary<string,string> Parameters { get; set; }
    }
}
