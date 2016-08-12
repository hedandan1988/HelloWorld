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
            Tools.MSSQL.Execute("INSERT INTO AdminLoginLog VALUES(@Admin,GETDATE(),NULL,@LoginIP)", new {Admin=admin.Id,LoginIP=Request.Host });
            throw new NotImplementedException();
        }
    }
}
