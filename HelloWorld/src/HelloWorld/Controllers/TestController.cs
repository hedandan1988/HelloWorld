using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basic.Library;
using System.Data.SqlClient;
using Dapper;
using Model.Library;

namespace HelloWorld.Controllers
{
    public class TestController : Controller
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public const string CONNECTIONSTRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HelloWorld;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public const string key = "juyhszco";

        public Admin AdminInfo { get; private set; }

        [Verify]
        // GET: /<controller>/
        public IActionResult Index(string input)
        {
            string md5 = EncryptUtils.MD5Encrypt(input);
            return Json(md5);
        }

        public IActionResult Admin() {
            using (var conn = new SqlConnection(CONNECTIONSTRING))
            {
                AdminInfo = conn.Query<Admin>("select top 1 * from admin").FirstOrDefault();
                if (AdminInfo == null || AdminInfo.Id == 0 || AdminInfo.Verify == (int)Status.AdminVerifyEnum.禁用)
                {
                    return new RedirectResult("/admin/login");
                }
                return Json(AdminInfo);
            }
        }
    }
}
