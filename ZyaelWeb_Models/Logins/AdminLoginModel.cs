using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.Logins
{
    public  class AdminLoginModel
    {
        public int Id { get; set; }
        public int returnId { get; set; }
        public int AdminUserID { get; set; }
        public int VendorUserID { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class VendorsCredentialModel
    {
        public int TotalrowCount { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string status { get; set; }
    }
        
}
