using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSQ.GiftEntertainmentTracker.Models;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using RSQ.GiftEntertainmentTracker.DataAccess;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult UserResult()
        {
            List<UserModel> users = DataAccess.UserDAL.GetUsers();
            BindDepartmentList();
            return View(users);
        }

        [HttpPost]
        public ActionResult UserResult(string department)
        {
            if (!string.IsNullOrEmpty(department))
            {
                // TODO: Add insert logic here
                int objectId = Convert.ToInt32(department);
                string objectTypeCode = Common.ObjectTypeCode.Department;
                return RedirectToAction("CreateUser", "User", new { objectId, objectTypeCode });
            }
            else
            {
                List<UserModel> users = DataAccess.UserDAL.GetUsers();
                BindDepartmentList();
                return View(users);
            }
        }

        void BindDepartmentList()
        {
            List<DepartmentModel> departments = DepartmentDAL.GetDepartments();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DepartmentModel d in departments)
            {
                if (!string.IsNullOrEmpty(d.DivisionName))
                    items.Add(new SelectListItem { Text = d.CompanyName + "," + d.DivisionName + "," + d.DepartmentName, Value = d.DepartmentId.ToString() });
                else
                    items.Add(new SelectListItem { Text = d.CompanyName + "," + "-" + "," + d.DepartmentName, Value = d.DepartmentId.ToString() });
            }
            ViewBag.Department = items;
        }

        //
        // GET: /User/Details/5

        public ActionResult UserDetails(int userId)
        {
            UserModel user = DataAccess.UserDAL.Get(userId);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult CreateUser(int objectId, string objectTypeCode)
        {
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult CreateUser(UserModel user, int objectId, string objectTypeCode)
        {
            user.ObjectId = objectId;
            user.ObjectTypeCode = objectTypeCode;
            user.AddedBy = User.Identity.Name;
            DataAccess.UserDAL.Insert(user);
            return RedirectToAction("UserResult", "User");
        }
        
        //
        // GET: /User/Edit/5

        public ActionResult EditUser(int userId)
        {
            UserModel user = DataAccess.UserDAL.Get(userId);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult EditUser(int userId, UserModel user)
        {

            DataAccess.UserDAL.Update(user);
            return RedirectToAction("UserResult", "User");
        }

        //
        // GET: /User/Delete/5

        public ActionResult DeleteUser(int userId)
        {
            DataAccess.UserDAL.Delete(userId);
            return RedirectToAction("UserResult", "User");
        }
    }
}
