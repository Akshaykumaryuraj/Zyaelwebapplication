using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.DigitalConsultationAdmin
{
    internal class DCAdminModel
    {
    }
    public class DoctorProfileModel
    {
        public int TotalrowCount { get; set; }

        public int DoctorPId { get; set; }
        public int DoctorID { get; set; }
        public int DCDOTP { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public String Studies { get; set; }
        public String Experience { get; set; }
        public String EmailAddress { get; set; }
        public Int64 PhoneNumber { get; set; }
        public String ConsultationCategory { get; set; }
        public String ConsultationFees { get; set; }
        public bool status { get; set; }
        public String ProficientLanguage { get; set; }
        public String Specialization { get; set; }
        public string DoctorBio { get; set; }
        public string DoctorBio_1 { get; set; }
        public string DoctorBio_2 { get; set; }
        public string DoctorProcedure { get; set; }
        public string DoctorProcedure_1 { get; set; }
        public string DoctorProcedure_2 { get; set; }
        //public string message { get; set; }
        //public int returnId { get; set; }
        public string DoctorIntroVideoLink { get; set; }
        //public List<achievements> achievements { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public String City { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        //public IFormFile DoctorProfileImage { get; set; }

        public string DoctorProfileImageName { get; set; }
        public string DoctorProfileImagePath { get; set; }
        public int DoctorProfileImageID { get; set; }
        public string AboutDoctor { get; set; }





    }
    public class DoctorActivationModel
    {
        public bool status { get; set; }
        public bool goLive { get; set; }
        public bool isActive { get; set; }
        public int DoctorID { get; set; }
        public int DCDOTP { get; set; }
        public int TotalrowCount { get; set; }
        public Int64 PhoneNumber { get; set; }


    }

    public class ConsultationSlotDateModel
    {

        public bool Available { get; set; }
        public string Time { get; set; }
        public int DoctorID { get; set; }

        public DateTime Date { get; set; }

    }


    public class UserAppointmentsModel
    {
        public int UAID { get; set; }
        public int TotalrowCount { get; set; }

        public int UserID { get; set; }
        public int DoctorID { get; set; }
        public int DoctorPId { get; set; }
        public DateTime Date { get; set; }
        public string AppointmentDate { get; set; }
        public string UserSelectedSlot { get; set; }
        public int UserRandomID { get; set; }
        public int ConsultationFees { get; set; }
        public String FirstName { get; set; }
        public String DoctorFirstName { get; set; }
        public String PatientName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public string DOB { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public Int64 PhoneNumber { get; set; }
        public String ConsultationCategory { get; set; }
        public string Allergies { get; set; }
        public string CurrentMedications { get; set; }
        public string PastMedications { get; set; }
        public string Injuries { get; set; }
        public string Surgeries { get; set; }
        public string Smoking { get; set; }
        public string Alcohol { get; set; }
        public string FoodPreference { get; set; }
        public string Occupation { get; set; }

        public string Status { get; set; }
        public int RoomID { get; set; }
        public string token { get; set; }
        //public string UserPrescriptionPath { get; set; }
        //public string UserReportPath { get; set; }
        public bool VideoCallJoinStatus { get; set; }
        public bool isDeleted { get; set; }





    }
}
