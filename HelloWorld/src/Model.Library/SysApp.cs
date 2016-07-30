using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Library
{
    public class SysApp
    {
        public SysApp() {
            Children = new List<SysApp>();
        }
        public int Id { get; set; }
        public int Level { get; set; }
        public int Verify { get; set; }
        public string AppName { get; set; }
        public string AppUrl { get; set; }
        public string ParentIds { get; set; }
        public string CssClass { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime Update { set; get; }
        public string AppActiveUrl { get; set; }
        public int Grade { get; set; }
        public List<SysApp> Children { get; set; }
        public bool IsActive { get; set; }
    }

}
