using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Models.DiagnosticLabs;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Services.DiagnosticLabs;
using ZyaelWeb_Services.InternalDoctor;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.DiagnosticLabs
{
        public class DiagnosticLabProductController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public DiagnosticLabProduct _diagnosticLabproduct;


        public DiagnosticLabProductController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _diagnosticLabproduct = new DiagnosticLabProduct(httpContextAccessor, config);
            

        }

        [HttpGet]
        public async Task<IActionResult> LabTestDetailsAdd(int LabTestID)
        {
            DiagnosisLabTestDetailsModel item = new DiagnosisLabTestDetailsModel();


            if (LabTestID > 0)
            {
                item = await _diagnosticLabproduct.LabTestDetailsAdd(LabTestID);

                item.LabTestID = LabTestID;
            }

            return View(item);
        }


        [HttpPost]
        public async Task<IActionResult> LabTestDetails_InsertUpdateByDiagnosisLab(DiagnosisLabTestDetailsModel item)

        {
            DiagnosisLabTestDetailsModel test = new DiagnosisLabTestDetailsModel();
            item.DLVID = DLVID;
            var result = await _diagnosticLabproduct.LabTestDetails_InsertUpdateByDiagnosisLab(item);
            return RedirectToAction("LabTestsGrid", "DiagnosticLabProduct");
        }


        public IActionResult LabTestsGrid()
        {
            DiagnosisLabTestDetailsModel item = new DiagnosisLabTestDetailsModel();
            return View(item);
        }



        [HttpPost]
        public async Task<IActionResult> getLabTestGridDetails(int pageNumber, int pageSize, int LabTestID)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<DiagnosisLabTestDetailsModel> list = new List<DiagnosisLabTestDetailsModel>();

            list = await _diagnosticLabproduct.getLabTestGridDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, DLVID, LabTestID);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }


        [HttpPost]
        public async Task<IActionResult> SetLabTestActiveStatus(DiagnosisLabTestDetailsModel item)
        {
            var result = await _diagnosticLabproduct.SetLabTestActiveStatus(item);
            return Json(result);
        }

    }
}
