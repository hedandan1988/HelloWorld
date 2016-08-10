using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.FrontModel
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageDataModel<T>
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Counts { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCounts { get; set; }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> DataList { get; set; }
    }
}
