using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Library
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public int Verify { get; set; }
        public int Points { get; set; }
        public int Sex { get; set; }
        public DateTime JoinDate { get; set; }
        public int AreaId { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
