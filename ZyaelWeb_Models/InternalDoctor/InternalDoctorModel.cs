using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace ZyaelWeb_Models.InternalDoctor
{
    public class InternalDoctorModel
    {
        public int IDoctorID { get; set; }
        public int HospitalVendorID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public String Studies { get; set; }
        public String Experience { get; set; }
        public Int64 Phone { get; set; }
        public String ConsultationCategory { get; set; }
        public String ConsultationFees { get; set; }
        public String status { get; set; }
        public String City { get; set; }
        public String ProficientLanguage { get; set; }
        public String Specialization { get; set; }
        public string DoctorBio { get; set; }
        public string DoctorBio_1 { get; set; }
        public string DoctorBio_2 { get; set; }
        public string DoctorProcedure { get; set; }
        public string DoctorProcedure_1 { get; set; }
        public string DoctorProcedure_2 { get; set; }
        public string message { get; set; }
        public int returnId { get; set; }
        public string DoctorIntroVideoLink { get; set; }
        //public List<achievements> achievements { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public int TotalrowCount { get; set; }
        public List<Shifts> Shifts { get; set; }



    }
    public class ShiftSlotModel
    {
        public int ShiftSlotID { get; set; }
        public bool Available { get; set; }
        public string SlotsAvailable { get; set; }
        public string Time { get; set; }
        public List<Shifts> Shifts { get; set; }
        public List<string> slottimings { get; set; }
        public int IDoctorID { get; set; }
        public string FirstName { get; set; }
        public int HospitalVendorID { get; set; }
        public int count { get; set; }
        public DateTime Date { get; set; }
        public List<SelectListItem> drpSubjects { get; set; }
    }

    public class Shifts
    {
        public string Time { get; set; }
        public bool Available { get; set; }
    }
}
