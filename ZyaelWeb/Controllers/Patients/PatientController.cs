using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Models.Patients;
using ZyaelWeb_Services.Hospital;
using ZyaelWeb_Services.InternalDoctor;
using ZyaelWeb_Services.Patients;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Patients
{
    public class PatientController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Patient _patient;


        public PatientController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _patient = new Patient(httpContextAccessor, config);
        }


        [HttpGet]
        public async Task<IActionResult> PatientRecords(int PatientID)
        {
            PatientModel item = new PatientModel();
            if (PatientID > 0)
            {
                item = await _patient.NewPatientRecordsAdd(PatientID);

                item.PatientID = PatientID;
            }

            return View(item);
        }



        [HttpPost]
        public async Task<IActionResult> NewPatientRecordDetails_InsertUpdate(PatientModel item)
        {
            PatientModel test = new PatientModel();
            item.HospitalVendorID = HospitalVendorID;
            var result = await _patient.NewPatientRecordDetails_InsertUpdate(item);

            return RedirectToAction("PatientDetailsGrid", "Patient");
        }

        public IActionResult PatientDetailsGrid()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> getPatientGridDetails(int pageNumber, int pageSize, int PatientID)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<PatientModel> list = new List<PatientModel>();

            list = await _patient.getPatientGridDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, HospitalVendorID, PatientID);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }


        public IActionResult PatientReportsGrid()
        {

            return View();
        }



    }
}
