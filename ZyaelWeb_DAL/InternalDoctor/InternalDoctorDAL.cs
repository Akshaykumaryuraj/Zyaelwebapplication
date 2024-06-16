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
using ZyaelWeb_Models.Logins;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.InternalDoctor
{
    public class InternalDoctorDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public InternalDoctorDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }

        public async Task<InternalDoctorModel> InternalDoctorDetailsAdd(int IDoctorID)
        {
            try
            {

                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            IDoctorID = IDoctorID

                        };
                    return (await con.QueryAsync<InternalDoctorModel>("SP_getInternalDoctorDetails", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> InternalDoctorDetails_InsertUpdate(InternalDoctorModel item)
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
                                IDoctorID = item.IDoctorID,
                                EmailAddress = item.EmailAddress,
                                UserName = item.UserName,
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Gender = item.Gender,
                                Studies = item.Studies,
                                Experience = item.Experience,
                                Phone = item.Phone,
                                ConsultationCategory = item.ConsultationCategory,
                                ConsultationFees = item.ConsultationFees,
                                City = item.City,
                                ProficientLanguage = item.ProficientLanguage,
                                Specialization = item.Specialization,
                                status = item.status,
                                DoctorBio = item.DoctorBio,
                                DoctorBio_1 = item.DoctorBio_1,
                                DoctorBio_2 = item.DoctorBio_2,
                                DoctorIntroVideoLink = item.DoctorIntroVideoLink,
                                DoctorProcedure = item.DoctorProcedure,
                                DoctorProcedure_1 = item.DoctorProcedure_1,
                                DoctorProcedure_2 = item.DoctorProcedure_2,
                                HospitalVendorID=item.HospitalVendorID

                            };
                    var response = await con.ExecuteScalarAsync<int>("Sp_SetInternalDoctorDetails", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<List<InternalDoctorModel>> getInternalDoctorsDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int HospitalVendorID, int IDoctorID)
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
                            IDoctorID= IDoctorID
                        };
                    return (await con.QueryAsync<InternalDoctorModel>("SP_getInternalDoctorDetailsList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SetInternalDoctorActiveStatus(InternalDoctorModel item)
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            status = item.status,
                            IDoctorID = item.IDoctorID
                        };
                    var response = await con.ExecuteScalarAsync<int>("SP_setInternalDoctorActiveStatus", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> SetInternalDoctorSlots(ShiftSlotModel item)
        {
            ShiftSlotModel result = new ShiftSlotModel();

            try
            {

              
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var del =
                           new
                           {
                               IDoctorID = item.IDoctorID,
                               Date = item.Date

                           };
                    await con.ExecuteScalarAsync<ShiftSlotModel>("SP_InternalDoctorSlotDetailsDelete", del, commandType: System.Data.CommandType.StoredProcedure);

                    DateTime Date = item.Date;
                    int IDoctorID = item.IDoctorID;
                    int HospitalVendorID = item.HospitalVendorID;
                    bool Available = item.Available;
                    foreach (var slottimings in item.slottimings)
                    {


                        var Param =
                                new
                                {
                                    IDoctorID = IDoctorID,
                                    HospitalVendorID = HospitalVendorID,
                                    Date = Date,
                                    Time = slottimings,
                                    Available = true


                                };
                        await con.ExecuteScalarAsync<int>("SP_SetInternalDoctorSlots", Param, commandType: System.Data.CommandType.StoredProcedure);

                    }
                    return 0;

                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<ShiftSlotModel> GetIDoctorDetails(int IDoctorID)
        {
            try
            {

                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            IDoctorID = IDoctorID

                        };
                    return (await con.QueryAsync<ShiftSlotModel>("SP_getInternalDoctorsByIDoctorID", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}

