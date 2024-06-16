using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.DiagnosticLabs;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.DiagnosticLabs
{
    public class DiagnosticLabProductDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public DiagnosticLabProductDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }

        public async Task<DiagnosisLabTestDetailsModel> LabTestDetailsAdd(int LabTestID)
        {
            try
            {

                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            LabTestID = LabTestID

                        };
                    return (await con.QueryAsync<DiagnosisLabTestDetailsModel>("SP_getLabtestDetailsByID", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> LabTestDetails_InsertUpdateByDiagnosisLab(DiagnosisLabTestDetailsModel item)
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
                                LabTestID = item.LabTestID,
                                DLVID = item.DLVID,
                                DLVPID = item.DLVPID,
                                LabTestName = item.LabTestName,
                                LabTestSubTitle_1 = item.LabTestSubTitle_1,
                                LabTestSubTitle_2 = item.LabTestSubTitle_2,
                                LabTestCode = item.LabTestCode,
                                LabTestPrice = item.LabTestPrice,
                                LabTestDiscountPrice = item.LabTestDiscountPrice,
                                LabTestOffer = item.LabTestOffer,
                                VisitForHome = item.VisitForHome,
                                VisitForCentre = item.VisitForCentre,
                                AboutLabTest_1 = item.AboutLabTest_1,
                                AboutLabTest_2 = item.AboutLabTest_2,
                                Prerequisites_1 = item.Prerequisites_1,
                                Prerequisites_2 = item.Prerequisites_2,
                                DiseasesCovered_1 = item.DiseasesCovered_1,
                                DiseasesCovered_2 = item.DiseasesCovered_2,
                                DiseasesCovered_3 = item.DiseasesCovered_3,
                                BodyFunction_1 = item.BodyFunction_1,
                                BodyFunction_2 = item.BodyFunction_2,
                                BodyFunction_3 = item.BodyFunction_3,
                                LifeStyle_1 = item.LifeStyle_1,
                                LifeStyle_2 = item.LifeStyle_2,
                                LifeStyle_3 = item.LifeStyle_3,
                                LabTestCategoryName = item.LabTestCategoryName,
                                LabTestReportIn = item.LabTestReportIn



                            };
                    var response = await con.ExecuteScalarAsync<int>("Sp_SetDiagnosisLabTestDetails", Param, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }



        public async Task<List<DiagnosisLabTestDetailsModel>> getLabTestGridDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int DLVID, int LabTestID)
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
                            DLVID = DLVID,
                            LabTestID = LabTestID
                        };
                    return (await con.QueryAsync<DiagnosisLabTestDetailsModel>("SP_getLabTestDetailsGridList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SetLabTestActiveStatus(DiagnosisLabTestDetailsModel item)
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
                            LabTestID = item.LabTestID
                        };
                    var response = await con.ExecuteScalarAsync<int>("SetLabTestActiveStatus", Param, commandType: System.Data.CommandType.StoredProcedure);
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
