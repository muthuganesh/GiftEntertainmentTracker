using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSQ.GiftEntertainmentTracker.Models;
using RSQ.GiftEntertainmentTracker.DataAccess;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class SuperAdminController : Controller
    {
        //
        // GET: /SuperAdmin/

        public ActionResult SuperAdminResult(int companyId)
        {
            List<UserModel> users = SuperAdminDAL.GetCompanyProfiles(companyId);
            return View(users);
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
            List<UserModel> users = SuperAdminDAL.GetCompanyProfiles(companyId);
            return View(users);
        } 

        //
        // POST: /SuperAdmin/Create

        [HttpPost]
        public ActionResult Create(string companyItems)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            string action = null, route = null;
            if (companyItems == Common.ObjectTypeCode.Divison)
            {
                action = "CreateDivision";
                route = "Division";
                goto send;
            }
            else if (companyItems == Common.ObjectTypeCode.Department)
            {
                action = "CreateDepartment";
                route = "Department";
                goto send;
            }
        send:
            return RedirectToAction(action, route, new { objectId = companyId, objectTypeCode = Common.ObjectTypeCode.Company });
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
            if (divisions.Count != 0)
                return View(divisions);
            else
                return View();
        }

        public ActionResult Department(int divisionId)
        {
            List<DepartmentModel> departments = DepartmentDAL.GetDepartments(divisionId, Common.ObjectTypeCode.Divison);

            if (departments.Count != 0)
                return View(departments);
            else
                return View();
        }

        public ActionResult Users(int departmentId)
        {
            List<UserModel> Users = UserDAL.GetUsers(departmentId, Common.ObjectTypeCode.Department);

            if (Users.Count != 0)
                return View(Users);
            else
                return View();
        }
    }
}
