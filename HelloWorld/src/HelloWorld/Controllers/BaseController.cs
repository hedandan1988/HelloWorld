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
using Microsoft.AspNetCore.Mvc.Routing;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorld.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 获得登录页对象
        /// </summary>
        /// <returns></returns>
        protected RedirectResult GetAdminLoginResult()
        {
            return new RedirectResult("/admin/login");
        }

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
        //public const string CONNECTIONSTRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HelloWorld;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public const string CONNECTIONSTRING = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = HelloWorld; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
                    AdminInfo = conn.Query<Admin>("select top 1 * from admin where UserName=@UserName and UserPwd=@UserPwd", new { UserName = adminname, UserPwd = adminpwd }).FirstOrDefault();
                    if (AdminInfo == null || AdminInfo.Id == 0 || AdminInfo.Verify == (int)Status.AdminVerifyEnum.禁用)
                    {
                        context.Result = GetAdminLoginResult();
                        context.Canceled = true;
                    }
                    //获取用户角色信息
                    SysRoleInfo = conn.Query<SysRole>("select top 1 * from SysRole where id=(select roleid from adminrole where adminid=@id and verify=@verify)", new { id = AdminInfo.Id, verify = (int)Status.SysRoleVerifyEnum.启用 }).FirstOrDefault();
                    if (SysRoleInfo == null || SysRoleInfo.Id == 0)
                    {
                        context.Result = GetAdminLoginResult();
                    }
                    //获取权限信息
                    SysAppList = conn.Query<SysApp>("select * from SysApp where id in(select appid from sysroleapp where verify=@verify and roleid=@id)", new { id = SysRoleInfo.Id, verify = (int)Status.SysAppVerifyEnum.启用 }).ToList();
                    //判断权限
                    string controller = context.RouteData.Values["controller"].ToString().ToLower();
                    string action = context.RouteData.Values["action"].ToString().ToLower();
                    string appActiveUrl = controller + "/" + action;
                    SysApp app = SysAppList.FirstOrDefault(o => o.AppActiveUrl.ToLower().Equals(appActiveUrl));
                    if (app == null)
                    {
                        context.Result = GetAdminLoginResult();
                    }
                    app.IsActive = true;
                    //分组
                    var root = SysAppList.Where(o => o.Level == 0).ToList();
                    setSysAppList(SysAppList, 0, root, appActiveUrl);
                    SysAppList = root;
                    ViewBag.AdminDataModel = new AdminDataModel
                    {
                        AdminInfo = AdminInfo,
                        SysAppList = SysAppList,
                        SysRoleInfo = SysRoleInfo
                    };
                }
            }
            else
            {
                context.Result = GetAdminLoginResult();
            }

        }

        private void setSysAppList(List<SysApp> sysAppList, int level, List<SysApp> root, string appActiveUrl)
        {
            if (root != null && root.Count > 0)
            {
                level++;
                foreach (var item in root)
                {
                    item.IsActive = item.AppActiveUrl.ToLower().Equals(appActiveUrl);
                    item.Children = new List<SysApp>();
                    var second = SysAppList.Where(o => o.ParentIds.Contains("," + item.Id + ",") && o.Level == level).ToList();
                    if (second != null && second.Count > 0)
                    {
                        var t = second.FirstOrDefault(p => p.AppActiveUrl.ToLower().Equals(appActiveUrl));
                        if (t != null)
                            item.IsActive = t.IsActive = true;
                    }
                    item.Children.AddRange(second);
                    setSysAppList(sysAppList, level, second, appActiveUrl);
                }
            }
        }
    }
}
