using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.Models;
using System.Web.Profile;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult UserResult()
        {
            return View();
        }

        //
        // GET: /Profile/Details/5

        public ActionResult UserDetails(string userName)
        {
            Users user = new Users();
            if (ProfileCommon.Create(HttpContext.User.Identity.Name.ToString()) as ProfileCommon == null ||
                ProfileCommon.Create(userName) as ProfileCommon == null)
                return View(user);
            else
            {
                user.FirstName = HttpContext.Profile.GetPropertyValue("FirstName").ToString();
                user.LastName = HttpContext.Profile.GetPropertyValue("LastName").ToString();
                user.AddressLine1 = HttpContext.Profile.GetPropertyValue("AddressLine1").ToString();
                user.AddressLine2 = HttpContext.Profile.GetPropertyValue("AddressLine2").ToString();
                user.City = HttpContext.Profile.GetPropertyValue("City").ToString();
                user.State = HttpContext.Profile.GetPropertyValue("State").ToString();
                user.Country = HttpContext.Profile.GetPropertyValue("Country").ToString();
                user.ZipCode = HttpContext.Profile.GetPropertyValue("ZipCode").ToString();
                user.PhoneNo = HttpContext.Profile.GetPropertyValue("PhoneNo").ToString();
                return View(user);
            }
        }

        //
        // GET: /Profile/Create

        public ActionResult CreateUser()
        {
            return View();
        } 

        //
        // POST: /Profile/Create

        [HttpPost]
        public ActionResult CreateUser(Users user)
        {
            MembershipUser mUser = Membership.GetUser();
            ProfileCommon profile = ProfileCommon.Create(mUser.UserName) as ProfileCommon;

            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.AddressLine1 = user.AddressLine1;
            profile.AddressLine2 = user.AddressLine2;
            profile.City = user.City;
            profile.State = user.State;
            profile.Country = user.Country;
            profile.ZipCode = user.ZipCode;
            profile.PhoneNo = user.PhoneNo;
            profile.Save();

            return RedirectToAction("Index", "Home");
        }
        
        //
        // GET: /Profile/Edit/5
 
        public ActionResult EditUser()
        {
            Users user = new Users();
            user.FirstName = HttpContext.Profile.GetPropertyValue("FirstName").ToString();
            user.LastName = HttpContext.Profile.GetPropertyValue("LastName").ToString();
            user.AddressLine1 = HttpContext.Profile.GetPropertyValue("AddressLine1").ToString();
            user.AddressLine2 = HttpContext.Profile.GetPropertyValue("AddressLine2").ToString();
            user.City = HttpContext.Profile.GetPropertyValue("City").ToString();
            user.State = HttpContext.Profile.GetPropertyValue("State").ToString();
            user.Country = HttpContext.Profile.GetPropertyValue("Country").ToString();
            user.ZipCode = HttpContext.Profile.GetPropertyValue("ZipCode").ToString();
            user.PhoneNo = HttpContext.Profile.GetPropertyValue("PhoneNo").ToString();
            return View(user);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult EditUser(Users user)
        {
            ProfileCommon profile = ProfileCommon.Create(HttpContext.User.Identity.Name.ToString()) as ProfileCommon;
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.AddressLine1 = user.AddressLine1;
            profile.AddressLine2 = user.AddressLine2;
            profile.City = user.City;
            profile.State = user.State;
            profile.Country = user.Country;
            profile.ZipCode = user.ZipCode;
            profile.PhoneNo = user.PhoneNo;
            profile.Save();

            return RedirectToAction("Index", "Home");
        }
        //
        // GET: /Profile/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
