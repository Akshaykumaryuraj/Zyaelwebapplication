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
    public class Login
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public LoginDAL _logindal;
        public Login(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _logindal = new LoginDAL(httpContextAccessor, config);
        }


        public async Task<HospitalsVendorsLoginModel> SetHospitalLogin(HospitalsVendorsLoginModel item)
        {
            try
            {
                var result = await _logindal.SetHospitalLogin(item);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

