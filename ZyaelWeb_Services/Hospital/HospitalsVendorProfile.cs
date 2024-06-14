using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Hospitals;
using ZyaelWeb_Models.Hospital;

namespace ZyaelWeb_Services.Hospital
{
    public class HospitalVendorProfile
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public HospitalsVendorProfileDAL _hospitalProfiledal;
        public HospitalVendorProfile(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _hospitalProfiledal = new HospitalsVendorProfileDAL(httpContextAccessor, config);
        }
        public async Task<HospitalModel> HospitalCredentialAdd(int HospitalVendorID)
        {
            try
            {
                var result = await _hospitalProfiledal.HospitalCredentialAdd(HospitalVendorID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
