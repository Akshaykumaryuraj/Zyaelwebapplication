using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.DiagnosticLabs
{
    public class DiagnosticLabProductModel
    {
    }
    public class DiagnosisLabTestDetailsModel
    {
        public int LabTestID { get; set; }
        public int DLVID { get; set; }
        public int DLVPID { get; set; }
        public string UserName { get; set; }

        public string LabTestName { get; set; }
        public string LabTestSubTitle_1 { get; set; }
        public string LabTestSubTitle_2 { get; set; }
        public string LabTestCode { get; set; }
        public int LabTestPrice { get; set; }
        public int LabTestDiscountPrice { get; set; }
        public string LabTestOffer { get; set; }
        public string LabTestReportIn { get; set; }
        public bool VisitForHome { get; set; }
        public bool VisitForCentre { get; set; }
        public string AboutLabTest_1 { get; set; }
        public string AboutLabTest_2 { get; set; }
        public string Prerequisites_1 { get; set; }
        public string Prerequisites_2 { get; set; }
        public string DiseasesCovered_1 { get; set; }
        public string DiseasesCovered_2 { get; set; }
        public string DiseasesCovered_3 { get; set; }
        public string BodyFunction_1 { get; set; }
        public string BodyFunction_2 { get; set; }
        public string BodyFunction_3 { get; set; }
        public string LifeStyle_1 { get; set; }
        public string LifeStyle_2 { get; set; }
        public string LifeStyle_3 { get; set; }
        public string LabTestCategoryName { get; set; }
        public int TotalrowCount { get; set; }
        public String status { get; set; }

    }
}
