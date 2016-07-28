using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model.Library;
using System.Data.SqlClient;
using Dapper;
using Basic.Library;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorld.Controllers
{
    public class BaseController : Controller
    {
        public Admin AdminInfo;
        /// <summary>
        /// 链接字符串
        /// </summary>
        public const string CONNECTIONSTRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HelloWorld;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            string adminname = "";
            string adminpwd = "";
            var cookies = context.HttpContext.Request.Cookies;
            //获取信息
            if (cookies != null && cookies.Count > 0 && cookies.TryGetValue("adminname", out adminname) && cookies.TryGetValue("adminpwd", out adminpwd)){
                using (var conn = new SqlConnection(CONNECTIONSTRING))
                {
                    AdminInfo = conn.Query<Admin>("select top 1 * from admin where UserName=@UserName and UserPwd=@UserPwd").FirstOrDefault();
                    if (AdminInfo == null || AdminInfo.Id == 0||AdminInfo.Verify==(int)Status.AdminVerifyEnum.禁用)
                    {
                        context.Result = new RedirectResult("/admin/login");
                    }
                }
            }
            else
            {
                context.Result = new RedirectResult("/admin/login");
            }
            
        }
    }
}
