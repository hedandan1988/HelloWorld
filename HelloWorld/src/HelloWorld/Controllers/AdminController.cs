using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorld.Tools;
using Basic.Library;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorld.Controllers
{
    public class AdminController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


    }
    public class AdminLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return Json(new { status = (int)Status.CommonStatusEnum.数据验证失败, msg = "用户名和者密码不能为空" });
            }
            var admin = Tools.MSSQL.QueryFirstOrDefault<Model.Library.Admin>("SELECT TOP 1 * FROM ADMIN WHERE USERNAME=@USERNAME", new { username = username });
            if (admin == null || admin.Id == 0 || !admin.UserPwd.Equals(EncryptUtils.MD5Encrypt(password)))
            {
                return Json(new { status = (int)Status.CommonStatusEnum.数据验证失败, msg = "用户名和者密码错误" });
            }
            //记录登录日志
            var op =new Microsoft.AspNetCore.Http.CookieOptions();
            op.Expires = new DateTimeOffset(DateTime.Now, new TimeSpan(1, 0, 0));
            op.Domain = "/";
            HttpContext.Response.Cookies.Append(Dic.Library.Cookies.管理员账户,admin.UserName,op);
            HttpContext.Response.Cookies.Append(Dic.Library.Cookies.管理员密码, admin.UserPwd,op);
            HttpContext.Response.Cookies.Append(Dic.Library.Cookies.管理员ID, admin.Id.ToString(),op);
            Tools.MSSQL.Execute("INSERT INTO AdminLoginLog VALUES(@Admin,GETDATE(),NULL,@LoginIP)", new {Admin=admin.Id,LoginIP=Request.Host });
            return Json(new { status = (int)Status.CommonStatusEnum.成功, msg = "登陆成功" });
        }
    }
}
