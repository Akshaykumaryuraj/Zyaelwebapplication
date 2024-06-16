using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Services.Patients;
using ZyaelWeb_Services.pharmacys;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Pharmacys
{
    public class PharmacyController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Pharmacy _pharmacy;


        public PharmacyController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _pharmacy = new Pharmacy(httpContextAccessor, config);
        }

        public IActionResult PharmacyDashBoard()
        {
            return View();
        }
    }
}
