using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Admin;
using ZyaelWeb_DAL.Logins;
using ZyaelWeb_Models.Logins;

namespace ZyaelWeb_Services.Admins
{
    public  class Admin
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public AdminDAL _admindal;
        public Admin(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _admindal = new AdminDAL(httpContextAccessor, config);
        }


        public async Task<List<VendorsCredentialModel>> getVendorsCredentialDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, string vendor)
        {
            try
            {
                var result = await _admindal.getVendorsCredentialDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, vendor);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> SetVendorsLoginStatus(AdminLoginModel item)
        {
            try
            {
                var result = await _admindal.SetVendorsLoginStatus(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
