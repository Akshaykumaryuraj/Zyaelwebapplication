using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Services.Appointments;
using ZyaelWeb_Services.DiagnosticLabs;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.DiagnosticLabs
{
   public class DiagnosticLabController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public DiagnosticLab _diagnosticLab;


        public DiagnosticLabController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _diagnosticLab = new DiagnosticLab(httpContextAccessor, config);


        }

        public IActionResult DiagnosticLabDashBoard()
        {
            return View();
        }

    }
}
