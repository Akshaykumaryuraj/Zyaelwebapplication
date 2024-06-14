using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZyaelWeb.Controllers
{
    public class BaseController : Controller
    {
        public int HospitalVendorID
        {
            get
            {
                try
                {
                        var HospitalVendorID = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value);
                        return HospitalVendorID;
                   

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public int EmployeeID
        {
            get
            {
                try
                {
                    //if (Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value) != null)
                    //{
                    var EmployeeID = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.SerialNumber).Value);
                    //var CurrentUserId = Convert.ToInt32(1);
                    return EmployeeID;
                    //}

                }
                catch (Exception ex)
                {
                    return 0;
                }
                //return 0;
            }
        }

        public long AdminUserID
        {
            get
            {
                try
                {
                    //if (ClaimTypes.Actor != null)
                    //{
                        var AdminUserID = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Actor).Value);
                        //var CurrentUserId = Convert.ToInt32(1);
                        return AdminUserID;
                    //}
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public long CurrentUserId
        {
            get
            {
                try
                {
                    //var CurrentUserId = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value);
                    var CurrentUserId = Convert.ToInt32(1);
                    return CurrentUserId;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public long ProfileHeadID
        {
            get
            {
                try
                {
                    var ProfileHeadID = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Upn).Value);

                    return ProfileHeadID;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public long BankUserID
        {
            get
            {
                try
                {
                    var BankUserID = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Spn).Value);

                    return BankUserID;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public string CampusUserName
        {
            get
            {
                try
                {
                    var CampusUserName = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value;

                    return CampusUserName;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


    }
}