﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.Admins;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Models.Logins;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.Admin
{
    public class AdminDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public AdminDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }
        public async Task<List<VendorsCredentialModel>> getVendorsCredentialDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, string vendor)
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
                            vendor = vendor
                        };
                    return (await con.QueryAsync<VendorsCredentialModel>("SP_getVendorsCredentialList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<int> SetVendorsLoginStatus(AdminLoginModel item)
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
                            Id = item.Id,
                            Vendor = item.Vendor
                        };
                    var response = await con.ExecuteScalarAsync<int>("SP_setVendorLoginStatus", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public async Task<SpecialitiesModel> SpecialitiesDetailsAdd(int SpecialityID)
        {
            try
            {

                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            SpecialityID = SpecialityID

                        };
                    return (await con.QueryAsync<SpecialitiesModel>("SP_getSpecilizationDetailsByID", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SpecialitiesDetails_InsertUpdate(SpecialitiesModel item)
        {

            try
            {
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                            new
                            {
                                SpecialityID = item.SpecialityID,
                                SpecialityName = item.SpecialityName,
                                SpecialityCode = item.SpecialityCode,
                                Symptoms = item.Symptoms,
                                status=item.status

                            };
                    var response = await con.ExecuteScalarAsync<int>("Sp_SetMastersSpecialitiesDetails", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }


        }

        public async Task<List<SpecialitiesModel>> getSpecializationsDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int SpecialityID)
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
                            SpecialityID = SpecialityID
                        };
                    return (await con.QueryAsync<SpecialitiesModel>("SP_getSpecializationDetailsList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SetSpecilizationStatus(SpecialitiesModel item)
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
                            SpecialityID = item.SpecialityID
                        };
                    var response = await con.ExecuteScalarAsync<int>("SP_setSpecilizationStatus", Param, commandType: System.Data.CommandType.StoredProcedure);
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
