using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Admin;
using ZyaelWeb_DAL.Logins;

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

    }
}
