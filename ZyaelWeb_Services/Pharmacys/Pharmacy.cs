using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Appointments;
using ZyaelWeb_DAL.Pharmacy;

namespace ZyaelWeb_Services.pharmacys
{
         public class Pharmacy
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public PharmacyDAL _pharmacydal;
        public Pharmacy(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _pharmacydal = new PharmacyDAL(httpContextAccessor, config);
        }


    
    }
}
