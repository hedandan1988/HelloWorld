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
        /// <summary>
        /// 当前管理员信息
        /// </summary>
        public Admin AdminInfo { set; get; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public SysRole SysRoleInfo { get; set; }
        /// <summary>
        /// 所有可用权限列表
        /// </summary>
        public List<SysApp> SysAppList { get; set; }
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
            if (cookies != null && cookies.Count > 0 && cookies.TryGetValue("adminname", out adminname) && cookies.TryGetValue("adminpwd", out adminpwd))
            {
                using (var conn = new SqlConnection(CONNECTIONSTRING))
                {
                    AdminInfo = conn.Query<Admin>("select top 1 * from admin where UserName=@UserName and UserPwd=@UserPwd",new { UserName=adminname,UserPwd=adminpwd}).FirstOrDefault();
                    if (AdminInfo == null || AdminInfo.Id == 0 || AdminInfo.Verify == (int)Status.AdminVerifyEnum.禁用)
                    {
                        context.Result = new RedirectResult("/admin/login");
                    }
                    //获取用户角色信息
                    SysRoleInfo = conn.Query<SysRole>("select top 1 * from SysRole where id=(select roleid from adminrole where adminid=@id and verify=@verify)", new { id = AdminInfo.Id, verify = (int)Status.SysRoleVerifyEnum.启用 }).FirstOrDefault();
                    if (SysRoleInfo == null || SysRoleInfo.Id == 0)
                    {
                        context.Result = new RedirectResult("/admin/login");
                    }
                    //获取权限信息
                    SysAppList = conn.Query<SysApp>("select * from SysApp where id in(select appid from sysroleapp where verify=@verify and roleid=@id)", new { id = SysRoleInfo.Id, verify = (int)Status.SysAppVerifyEnum.启用 }).ToList();
                    //判断权限
                    string controller = context.RouteData.Values["controller"].ToString().ToLower();
                    string action=context.RouteData.Values["action"].ToString().ToLower();
                    SysApp app = SysAppList.FirstOrDefault(o => o.AppUrl.ToLower().Equals(controller + "/" + action));
                    if (app==null)
                    {
                        context.Result = new RedirectResult("/admin/login");
                    }
                    ViewBag.AdminDataModel = new AdminDataModel {
                         AdminInfo=AdminInfo, SysAppList=SysAppList, SysRoleInfo=SysRoleInfo
                    };
                }
            }
            else
            {
                context.Result = new RedirectResult("/admin/login");
            }

        }
    }
}
