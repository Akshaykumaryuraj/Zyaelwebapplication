using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.Patients
{
    public class PatientModel
    {
        public int HospitalVendorID { get; set; }
        public int PatientID { get; set; }
        public String Section { get; set; }
        public String BillType { get; set; }
        public long BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public String PatientName { get; set; }
        public long PatientMobileNumber{ get; set; }
        public long PatientRegNo { get; set; }
        public String PatientLoc { get; set; }
        public String PatientDoc { get; set; }
        public int PatientAge { get; set; }
        public String PatientGender { get; set; }
        public String Department { get; set; }
        public String Plan_Scheme { get; set; }
        public String DoctorName { get; set; }
        public int Charges { get; set; }
        public int Units { get; set; }
        public int Amount { get; set; }
        public String BillGenaratedBy { get; set; }
        public String ServiceName { get; set; }
        public int TotalrowCount { get; set; }



    }
}
