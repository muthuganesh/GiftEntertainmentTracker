using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.Models;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class DepartmentUserController : Controller
    {
        //
        // GET: /DepartmentUsers/

        public ActionResult DepartmentUsersResult()
        {
            List<DepartmentUserModel> departmentUsers = DataAccess.DepartmentUserDAL.GetDepartmentUsers();
            BindDepartmentUsers();
            return View(departmentUsers);
        }

        private void BindDepartmentUsers()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (MembershipUser m in Membership.GetAllUsers())
            {
                items.Add(new SelectListItem { Text = m.UserName, Value = m.UserName });
            }
            ViewBag.UserName = items;
        }

        [HttpPost]
        public ActionResult DepartmentUsersResult(string userName)
        {
            int departmentId = Convert.ToInt32(Session["ObjectId"]);
            DepartmentUserModel departmentUser = new DepartmentUserModel();
            
            if (!string.IsNullOrEmpty(userName))
            {
                departmentUser.UserName = userName;
                departmentUser.ObjectId = departmentId;
                departmentUser.ObjectTypeCode = Common.ObjectTypeCode.Department;
                departmentUser.AddedBy = HttpContext.User.Identity.Name;
                DataAccess.DepartmentUserDAL.Insert(departmentUser);
            }

            BindDepartmentUsers();
            List<DepartmentUserModel> departmentUsers = DataAccess.DepartmentUserDAL.GetDepartmentUsers();
            return View(departmentUsers);
        }

        //
        // GET: /DepartmentUsers/Details/5

        public ActionResult DepartmentUsersDetails(string userName)
        {
            return RedirectToAction("UserDetails", "Profile", new { userName = userName });
        }

        //
        // GET: /DepartmentUsers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /DepartmentUsers/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /DepartmentUsers/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DepartmentUsers/Edit/5

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
        // POST: /DepartmentUsers/Delete/5

        public ActionResult DepartmentUsers(int userId)
        {
            DataAccess.DepartmentUserDAL.Delete(userId);
            return RedirectToAction("DepartmentUsersResult");
        }
    }
}
