using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.InternalDoctor;
using ZyaelWeb_DAL.Patients;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Models.Patients;

namespace ZyaelWeb_Services.Patients
{
        public class Patient
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public PatientDAL _patientdal;
        public Patient(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _patientdal = new PatientDAL(httpContextAccessor, config);
        }

        public async Task<PatientModel> NewPatientRecordsAdd(int PatientID)
        {
            try
            {
                var result = await _patientdal.NewPatientRecordsAdd(PatientID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> NewPatientRecordDetails_InsertUpdate(PatientModel item)
        {
            try
            {
                var result = await _patientdal.NewPatientRecordDetails_InsertUpdate(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<PatientModel>> getPatientGridDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int HospitalVendorID, int PatientID)
        {
            try
            {
                var result = await _patientdal.getPatientGridDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, HospitalVendorID, PatientID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
