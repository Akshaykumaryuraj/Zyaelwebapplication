using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Models.Patients;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.Patients
{
   public class PatientDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public PatientDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }

        public async Task<PatientModel> NewPatientRecordsAdd(int PatientID)
        {
            try
            {

                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            PatientID = PatientID

                        };
                    return (await con.QueryAsync<PatientModel>("SP_getNewPatientRecordDetails", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
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
                var Connection = new SqlConnection(_config.GetConnectionString("DefautConnection"));
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                            new
                            {
                                PatientID = item.PatientID,
                                HospitalVendorID=item.HospitalVendorID,
                                PatientName = item.PatientName,
                                PatientRegNo = item.PatientRegNo,
                                PatientLoc = item.PatientLoc,
                                PatientDoc = item.PatientDoc,
                                PatientAge = item.PatientAge,
                                PatientGender = item.PatientGender,
                                PatientMobileNumber = item.PatientMobileNumber,
                                Department = item.Department,
                                Plan_Scheme = item.Plan_Scheme,
                                ServiceName = item.ServiceName,
                                Charges = item.Charges,
                                Units = item.Units,
                                Amount = item.Amount,
                                BillGenaratedBy = item.BillGenaratedBy,
                                DoctorName = item.DoctorName,
                                Section = item.Section,
                                BillType = item.BillType,
                                BillNumber = item.BillNumber
                             

                            };
                    var response = await con.ExecuteScalarAsync<int>("Sp_SetNewPatientRecordDetails", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
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
                string search = string.Empty;
                //if (!string.IsNullOrEmpty(searchinputText))
                //{
                //    search = "and (InstitutionName like'%" + searchinputText + "%')";
                //}
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            pageNumber = pageNumber,
                            pageSize = pageSize,
                            sortBy = sortBy,
                            sortingOrder = sortingOrder,
                            searchinputText = search,
                            HospitalVendorID = HospitalVendorID,
                            PatientID = PatientID
                        };
                    return (await con.QueryAsync<PatientModel>("SP_getPatientGridDetailsList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
