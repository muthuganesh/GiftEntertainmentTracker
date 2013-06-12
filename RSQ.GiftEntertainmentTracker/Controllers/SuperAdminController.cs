using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSQ.GiftEntertainmentTracker.Models;
using RSQ.GiftEntertainmentTracker.DataAccess;
using System.Web.Security;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    [Authorize]
    public class SuperAdminController : Controller
    {
        //
        // GET: /SuperAdmin/

        public ActionResult SuperAdminResult()
        {
            //List<UserModel> users = SuperAdminDAL.GetCompanyProfiles(companyId);
            return View();
        }

        //
        // GET: /SuperAdmin/Details/5

        public ActionResult Details()
        {
            return View();
        }

        //
        // GET: /SuperAdmin/Create

        public ActionResult Create(int companyId)
        {
            //List<UserModel> users = SuperAdminDAL.GetCompanyProfiles(companyId);
            return View();
        } 
        
        //
        // GET: /SuperAdmin/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SuperAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SuperAdmin/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SuperAdmin/Delete/5

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

        public ActionResult Division(int companyId)
        {
            List<DivisionModel> divisions = DivisionDAL.GetDivisions(companyId, Common.ObjectTypeCode.Company);
            Session["CompanyId"] = companyId;
            if (divisions.Count != 0)
                return View(divisions);
            else
                return View();
        }

        public ActionResult CreateDivision()
        {
            int companyId = Convert.ToInt32(Session["CompanyId"]);
            return RedirectToAction("CreateDivision", "Division", new { objectId = companyId, objectTypeCode = Common.ObjectTypeCode.Company });
        }

        public ActionResult DeleteDivision(int divisionId)
        {
            return RedirectToAction("DeleteDivision", "Division", new { divisionId = divisionId });
        }

        public ActionResult Department(int divisionId)
        {
            List<DepartmentModel> departments = DepartmentDAL.GetDepartments(divisionId, Common.ObjectTypeCode.Divison);
            Session["DivisionId"] = divisionId;
            if (departments.Count != 0)
                return View(departments);
            else
                return View();
        }

        public ActionResult CreateDepartment()
        {
            int divisionId = Convert.ToInt32(Session["DivisionId"]);
            return RedirectToAction("CreateDepartment", "Department", new { objectId = divisionId, objectTypeCode = Common.ObjectTypeCode.Divison });
        }

        public ActionResult DeleteDepartment(int departmentId)
        {
            return RedirectToAction("DeleteDepartment", "Department", new { departmentId = departmentId });
        }

        public ActionResult Users(int departmentId)
        {
            //List<UserModel> Users = UserDAL.GetUsers(departmentId, Common.ObjectTypeCode.Department);
            //Session["DepartmentId"] = departmentId;
            //BindUserList();
            //if (Users.Count != 0)
            //    return View(Users);
            //else
            //    return View();
            return View();
        }

        public ActionResult CreateUsers()
        {
            int departmentId = Convert.ToInt32(Session["DepartmentId"]);
            return RedirectToAction("CreateUser", "User", new { objectId = departmentId, objectTypeCode = Common.ObjectTypeCode.Department });
        }

        void BindUserList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (MembershipUser m in Membership.GetAllUsers())
            {
                items.Add(new SelectListItem { Text = m.UserName, Value = m.UserName });
            }
            ViewBag.UserName = items;
        }
    }
}
