using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZyaelWeb_Models.Hospital;
using ZyaelWeb_Services.Admins;
using ZyaelWeb_Services.Hospital;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Hospitals
{
    public class HospitalsController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public Hospital _hospital;
        public HospitalVendorProfile _hospitalvendorprofile;


        public HospitalsController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _hospital = new Hospital(httpContextAccessor, config);
            _hospitalvendorprofile = new HospitalVendorProfile(httpContextAccessor, config);


        }


        public IActionResult HospitalDashBoard()
        {
            return View();
        }

        public async Task<IActionResult> HospitalProfile()
        {
            HospitalModel item = new HospitalModel();
            var HospitalVendorID = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value);
            item = await _hospitalvendorprofile.HospitalCredentialAdd(HospitalVendorID);
            return View(item);
        }

    }
}
