using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Logins;
using ZyaelWeb_Models.Logins;

namespace ZyaelWeb_Services.Logins
{
    public class AdminLogin
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public AdminLoginDAL _adminlogindal;
        public AdminLogin(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _adminlogindal = new AdminLoginDAL(httpContextAccessor, config);
        }


        public async Task<AdminLoginModel> SetAdminLogin(AdminLoginModel item)
        {
            try
            {
                var result = await _adminlogindal.SetAdminLogin(item);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
