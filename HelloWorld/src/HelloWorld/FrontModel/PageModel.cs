using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.FrontModel
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageModel<T>
    {
        public List<T> Data { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageTotal { get; set; }
    }
}
