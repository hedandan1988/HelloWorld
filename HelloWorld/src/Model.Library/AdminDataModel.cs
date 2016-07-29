using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Library
{
    public class AdminDataModel
    {
        public Admin AdminInfo { get; set; }
        public SysRole SysRoleInfo { get; set; }
        public List<SysApp> SysAppList { get; set; }
    }
}
