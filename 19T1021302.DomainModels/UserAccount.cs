using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021302.DomainModels
{
    /// <summary>
    /// Quản lí thông tin của người dùng 
    /// </summary>
    public class UserAccount
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleNames { get; set; }
        public string Photos { get; set; }
        public string HashedPassword { get; set; }
        public bool Confirmed { get; set; }
    }
}
///Authentication: Kiểm tra ticket(username,password,...)
///
///Authorization : Kiểm tra tính phân quyền