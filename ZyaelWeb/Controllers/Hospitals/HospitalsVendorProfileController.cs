using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Models.Hospital;
using ZyaelWeb_Services.Hospital;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Hospitals
{
    public class HospitalsVendorProfileController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public HospitalVendorProfile _hospitalvendorprofile;


        public HospitalsVendorProfileController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _hospitalvendorprofile = new HospitalVendorProfile(httpContextAccessor, config);

        }

        //public IActionResult HospitalVendorProfile()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> HospitalCredentialAdd(int HospitalVendorID)
        {
            HospitalModel item = new HospitalModel();
            if (HospitalVendorID > 0)
            {
                item = await _hospitalvendorprofile.HospitalCredentialAdd(1);

                item.HospitalVendorID = HospitalVendorID;
            }
            return View(item);

        }

    }
}
