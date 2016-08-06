using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.FrontModel
{
    /// <summary>
    /// 分页数据模型
    /// </summary>
    public class PageDataModel
    {
        private int _pagesize = 10;
        private int _page = 1;
        /// <summary>
        /// 连接的表名，即FROM后面的内容
        /// </summary>
        public string TblName { get; set; }
        /// <summary>
        /// 要查询的字段名称，默认为全部
        /// </summary>
        public string FldName { get; set; }
        /// <summary>
        /// 排序字段，不需要ORDER BY，排序自行设置，请加入ASC或者DESC
        /// </summary>
        public string FldSort { get; set; }
        /// <summary>
        /// 要查询的语句，不需要WHERE，前面不需要跟AND或者OR, 但是不会影响计算
        /// </summary>
        public string StrCondition { get; set; }
        /// <summary>
        /// 要聚合的语句，不需要GROUP BY
        /// </summary>
        public string StrGroup { get; set; }
        /// <summary>
        /// HAVING语句，不需要HAVING
        /// </summary>
        public string StrHaving { get; set; }
        /// <summary>
        /// 是否去除重复数据，0不去除 / 1去除
        /// </summary>
        public int Dist { get; set; }
        /// <summary>
        /// 一个排序字段，当@FldSort为空时必须指定。而且该字段用于计算总条数，该字段为空时选取@FldSort的第一个字段
        /// </summary>
        public string StrOrder { get; set; }
        /// <summary>
        /// 是否只返回总条数而不进行分页,默认0 都返回
        /// </summary>
        public int OnlyCounts { get; set; }
        /// <summary>
        /// 每页要显示的数量,默认10
        /// </summary>
        public int PageSize {
            get
            {
                return _pagesize;
            }
            set
            {
                if (value <= 0)
                {
                    value = 10;
                }
                _pagesize = value;
            }
        }
        /// <summary>
        /// 要显示那一页的数据,默认1
        /// </summary>
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                _page = value;
            }
        }
        /// <summary>
        /// 返回总条数,输出参数
        /// </summary>
        public int Counts { get; set; }
        /// <summary>
        /// 返回总页数,输出参数
        /// </summary>
        public int PageCounts { get; set; }

    }
}
