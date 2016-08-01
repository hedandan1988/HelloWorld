using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorld.FrontModel;
using HelloWorld.Tools;
using Dapper;
using Model.Library;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorld.Controllers
{
    public class UserController : BaseController
    {
        
        public IActionResult List()
        {
            List<User> users = new List<Model.Library.User>();
            using (var conn = Connection) {
                string condition = " where 1=1";
                string kw = SearchInfo.GetValue("keyword");
                if (kw.IsNullOrEmpty())
                {
                    condition +=string.Format(" and username like '%{0}%'",kw);
                }
                string sql = "select * from [user] " + condition + " order by id desc";
                users= conn.Query<User>(sql).Skip(SearchInfo.PageSize * SearchInfo.PageIndex - 1).Take(SearchInfo.PageSize).ToList();
            }
            return View(users);
        }
    }
}
