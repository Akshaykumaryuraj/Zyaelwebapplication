using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.DigitalConsultationAdmin;
using ZyaelWeb_Models.Logins;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.DigitalConsultationAdmin
{
    public class DCAdminDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public DCAdminDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }


        public async Task<List<DoctorProfileModel>> getDCDoctorProfileDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText,int DoctorID)
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
                            DoctorID = DoctorID
                        };
                    return (await con.QueryAsync<DoctorProfileModel>("SP_getDCDoctorProfileList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DoctorProfileModel> DCDProfileDetailsAdd(int DoctorPId)
        {
            try
            {

                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            DoctorPId = DoctorPId

                        };
                    return (await con.QueryAsync<DoctorProfileModel>("Sp_GetDoctorsProfileDetailsbyId", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> DCDoctorProfileDetails_InsertUpdate(DoctorProfileModel item)
        {

            try
            {
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                            new

                            {
                                DoctorPId = item.DoctorPId,
                                DoctorID = item.DoctorID,
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Gender = item.Gender,
                                Studies = item.Studies,
                                EmailAddress = item.EmailAddress,
                                Experience = item.Experience,
                                PhoneNumber = item.PhoneNumber,
                                ConsultationCategory = item.ConsultationCategory,
                                ConsultationFees = item.ConsultationFees,
                                City = item.City,
                                Latitude = item.Latitude,
                                Longitude = item.Longitude,
                                Address_1 = item.Address_1,
                                Address_2 = item.Address_2,
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
                                AboutDoctor = item.AboutDoctor
                             


                            };
                    var response = await con.ExecuteScalarAsync<int>("Sp_SetDoctorProfileDetails", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public async Task<int> SetDoctorProfileStatus(DoctorProfileModel item)
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
                            DoctorPId = item.DoctorPId
                        };
                    var response = await con.ExecuteScalarAsync<int>("SP_SetDoctorProfileStatus", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public async Task<List<DoctorActivationModel>> getDCDoctorActiveDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int DoctorID)
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
                            DoctorID = DoctorID
                        };
                    return (await con.QueryAsync<DoctorActivationModel>("SP_getDCDoctorActiveList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SetDoctorActiveStatus(DoctorActivationModel item)
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            isActive = item.isActive,
                            DoctorID = item.DoctorID
                        };
                    var response = await con.ExecuteScalarAsync<int>("SP_SetWebDoctorActiveStatus", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> SetDoctorGoLiveStatus(DoctorActivationModel item)
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            goLive = item.goLive,
                            DoctorID = item.DoctorID
                        };
                    var response = await con.ExecuteScalarAsync<int>("SP_SetWebDoctorgoLiveStatus", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}
