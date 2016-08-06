using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorld.FrontModel;
using HelloWorld.Tools;
using Dapper;
using Model.Library;
using System.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorld.Controllers
{
    public class UserController : BaseController
    {

        public IActionResult List()
        {
            //List<User> users = new List<Model.Library.User>();
            //using (var conn = Connection) {
            //    string condition = " where 1=1";
            //    string kw = SearchInfo.GetValue("keyword");
            //    if (kw.IsNullOrEmpty())
            //    {
            //        condition +=string.Format(" and username like '%{0}%'",kw);
            //    }
            //    string sql = "select * from [user] " + condition + " order by id desc";
            //    users= conn.Query<User>(sql).Skip(SearchInfo.PageSize * SearchInfo.PageIndex - 1).Take(SearchInfo.PageSize).ToList();
            //}
            return View();
        }

        public IActionResult UserList()
        {
            List<User> users = new List<Model.Library.User>();
            using (var conn = Connection)
            {
                string condition = " where 1=1";
                string kw = SearchInfo.GetValue("keyword");
                if (!kw.IsNullOrEmpty())
                {
                    condition += string.Format(" and username like '%{0}%'", kw);
                }
                string sql = "select * from [user] " + condition + " order by id desc";
                //              EXEC @return_value = [dbo].[MyPageRead]

                //      @TblName = N'[user]',
                //@FldName = N'*',
                //@FldSort = N'id desc',
                //@strCondition = N'id>0',
                //@OnlyCounts = 0,
                //@PageSize = 100,
                //@Page = 1,
                //@Counts = @Counts OUTPUT,
                //@PageCounts = @PageCounts OUTPUT
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@TblName", "[user]");
                dp.Add("@FldName", "*");
                dp.Add("@FldSort", "id desc");
                dp.Add("@strCondition", "*");
                dp.Add("@FldSort", "id desc");
                dp.Add("@recordTotal", "", DbType.String, ParameterDirection.Output);

                var data = conn.Query("[MyPageRead]", dp, null, true, null, System.Data.CommandType.StoredProcedure);
                if (data == null)
                {

                }
            }
            return Json(users);
        }

        private DynamicParameters GetDynamicParameters(PageDataModel model)
        {
            //            @TblName nvarchar(3000)--连接的表名，即FROM后面的内容
            //, @FldName nvarchar(3000) = '*'--要查询的字段名称，默认为全部
            //, @FldSort nvarchar(3000) = NULL--排序字段，不需要ORDER BY，排序自行设置，请加入ASC或者DESC
            //, @strCondition nvarchar(3000) = NULL--要查询的语句，不需要WHERE，前面不需要跟AND或者OR, 但是不会影响计算
            //, @strGroup nvarchar(3000) = NULL--要聚合的语句，不需要GROUP BY
            //, @strHaving nvarchar(3000) = NULL--HAVING语句，不需要HAVING
            //, @Dist bit = 0--是否去除重复数据，0不去除 / 1去除
            //, @strOrder nvarchar(1000) = NULL--一个排序字段，当@FldSort为空时必须指定。而且该字段用于计算总条数，该字段为空时选取@FldSort的第一个字段
            //, @OnlyCounts bit = 0--是否只返回总条数而不进行分页

            //, @PageSize int = 10--每页要显示的数量
            //, @Page int = 1--要显示那一页的数据

            //, @Counts int = 1 output--返回总条数
            //, @PageCounts int = 1 output--返回总页数

        }
    }
}
