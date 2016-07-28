using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloWorld.Controllers
{
    public class VerifyAttribute : ActionFilterAttribute
    {
        public VerifyAttribute()
        {
            dicData = new Dictionary<string, string>();
        }
        public Dictionary<string, string> dicData;
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            string chkvalue = "";
            if (context.HttpContext.Request.Method.ToLower() == "get")
            {
                foreach (var item in context.HttpContext.Request.Query)
                {
                    if (item.Key.ToLower() == "chkvalue")
                    {
                        chkvalue = item.Value;
                        continue;
                    }
                    if (dicData.ContainsKey(item.Key.ToLower()))
                    {
                        dicData[item.Key.ToLower()] = dicData[item.Key] + "," + item.Value;
                    }
                    else
                        dicData.Add(item.Key.ToLower(), item.Value);
                }
            }
            else
            {
                foreach (var item in context.HttpContext.Request.Form)
                {
                    if (item.Key.ToLower() == "chkvalue")
                    {
                        chkvalue = item.Value;
                        continue;
                    }
                    if (dicData.ContainsKey(item.Key.ToLower()))
                    {
                        dicData[item.Key.ToLower()] = dicData[item.Key] + "," + item.Value;
                    }
                    else
                        dicData.Add(item.Key.ToLower(), item.Value);
                }
            }
            var s = dicData.OrderBy(o => o.Key);
            string str = "";
            foreach (var item in s)
            {
                str = str + item.Key + "=" + item.Value + "&";
            }
            str = str.TrimEnd('&');
            string compare = Basic.Library.EncryptUtils.MD5Encrypt(str, System.Text.Encoding.UTF8);
            dicData.Clear();
            if (!compare.ToLower().Equals(chkvalue))
            {
                context.Result = new JsonResult(new { status = (int)Basic.Library.Status.CommonStatusEnum.验证签名失败 });
                context.ExceptionHandled = true;
            }
        }
    }
}