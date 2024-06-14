using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Admin;
using ZyaelWeb_DAL.Hospitals;

namespace ZyaelWeb_Services.Hospital
{
    public class Hospital
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public HospitalsDAL _hospitaldal;
        public Hospital(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _hospitaldal = new HospitalsDAL(httpContextAccessor, config);
        }
    }
}
