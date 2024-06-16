using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.DiagnosticLabs;
using ZyaelWeb_DAL.InternalDoctor;
using ZyaelWeb_Models.DiagnosticLabs;
using ZyaelWeb_Models.InternalDoctor;

namespace ZyaelWeb_Services.DiagnosticLabs
{

         public class DiagnosticLabProduct
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public DiagnosticLabProductDAL _diagnosticlabproductLdal;
        public DiagnosticLabProduct(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _diagnosticlabproductLdal = new DiagnosticLabProductDAL(httpContextAccessor, config);
        }




        public async Task<DiagnosisLabTestDetailsModel> LabTestDetailsAdd(int LabTestID)
        {
            try
            {
                var result = await _diagnosticlabproductLdal.LabTestDetailsAdd(LabTestID);
                return result;
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
                var result = await _diagnosticlabproductLdal.LabTestDetails_InsertUpdateByDiagnosisLab(item);
                return result;
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
                var result = await _diagnosticlabproductLdal.getLabTestGridDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, DLVID, LabTestID);
                return result;
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
                var result = await _diagnosticlabproductLdal.SetLabTestActiveStatus(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
