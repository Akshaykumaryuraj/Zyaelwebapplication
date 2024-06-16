using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.Logins;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.Logins
{
    public class LoginDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public LoginDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }
        public async Task<HospitalsVendorsLoginModel> SetHospitalLogin(HospitalsVendorsLoginModel item)
        {
            //var password = common.PasswordEncription(item.Password);
            try
            {
                var Connection = new SqlConnection(_config.GetConnectionString("DefautConnection"));
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {

                            HospitalVendorEmail = item.HospitalVendorEmail,
                            HospitalVendorPassword = item.HospitalVendorPassword

                        };
                    return (await con.QueryAsync<HospitalsVendorsLoginModel>("sp_checkHospitalsVendorLoginDetails", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DiagnosticLabVendorsLoginModel> SetDiagnosticLabLogin(DiagnosticLabVendorsLoginModel item)
        {
            //var password = common.PasswordEncription(item.Password);
            try
            {
                var Connection = new SqlConnection(_config.GetConnectionString("DefautConnection"));
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {

                            //DiagnosticLabVendorEmail = item.DiagnosticLabVendorEmail,
                            //DiagnosticLabVendorPassword = item.DiagnosticLabVendorPassword
                            Email = item.Email,
                            Password = item.Password

                        };
                    return (await con.QueryAsync<DiagnosticLabVendorsLoginModel>("sp_checkDiagnosticLabLoginDetails", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<PharmacyVendorsLoginModel> SetPharmacyLogin(PharmacyVendorsLoginModel item)
        {
            //var password = common.PasswordEncription(item.Password);
            try
            {
                var Connection = new SqlConnection(_config.GetConnectionString("DefautConnection"));
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {

                            Email = item.Email,
                            Password = item.Password

                        };
                    return (await con.QueryAsync<PharmacyVendorsLoginModel>("sp_checkPharmacyVendorLoginDetails", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}

