using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Library
{
    public class ViewAdmin:Admin
    {
        public string RoleName { get; set; }
        public int RoleVerify { get; set; }
        public string AppName { get; set; }
        public string AppUrl { get; set; }
        public string CssClass { get; set; }
        public int Level { get; set; }
        public string ParentIds { get; set; }
        public int AppVerify { get; set; }
    }
}
