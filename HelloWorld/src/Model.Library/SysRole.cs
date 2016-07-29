using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Library
{
    public class SysRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int Verify { get; set; }
        public string Remark { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
