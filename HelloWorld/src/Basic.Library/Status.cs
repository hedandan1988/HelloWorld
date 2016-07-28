using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basic.Library
{
    public class Status
    {
        public enum CommonStatusEnum
        {
            验证签名失败=501,
            成功=200
        }
        public enum AdminVerifyEnum
        {
            启用 = 1,
            禁用 = 0
        }
    }
}
