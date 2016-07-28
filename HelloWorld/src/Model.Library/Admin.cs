using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Library
{
    public class Admin
    {
        public Admin()
        {
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Remark { get; set; }
        public string Country { get; set; }
        public string ProfileImg { get; set; }
        public DateTime JoinDate { get; set; }
        public int Verify { get; set; }
        public int Purview { get; set; }
        public string NickName { get; set; }
        public string TrueName { get; set; }


    }
}
