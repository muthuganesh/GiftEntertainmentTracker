using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSQ.GiftEntertainmentTracker.Models;
using RSQ.GiftEntertainmentTracker.DataAccess;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.Security;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        public ActionResult DepartmentResult()
        {
            string[] role = Roles.GetRolesForUser(User.Identity.Name);
            if (role.Count() > 0)
            {
                foreach (string r in role)
                {
                    var rolePermissions = RolePremissionSetDAL.GetRolePermissionSets(r);
                    if (rolePermissions != null)
                    {
                        DepartmentPermission dep = new DepartmentPermission(rolePermissions);

                        if (dep.CanICreateDepartment)
                            ViewBag.DepartmentCreate = dep.CanICreateDepartment;

                        if (dep.CanIEditDepartment)
                            ViewData["DepartmentEdit"] = string.Format(Common.ListItem.edit);
                        else
                            ViewData["DepartmentEdit"] = string.Format(Common.ListItem.nullValue);

                        if (dep.CanIDeleteDepartment)
                            ViewData["DepartmentDelete"] = string.Format(Common.ListItem.delete);
                        else
                            ViewData["DepartmentDelete"] = string.Format(Common.ListItem.nullValue);
                    }
                }
            }
            else
            {
                ViewData["DepartmentEdit"] = string.Format(Common.ListItem.edit);
                ViewData["DepartmentDelete"] = string.Format(Common.ListItem.delete);
            }
            int? objectId = Convert.ToInt32(Session["ObjectId"]);
            List<DepartmentModel> department = new List<DepartmentModel>();


            if (objectId.HasValue && objectId != 0)
                department = DataAccess.DepartmentDAL.GetDepartments(objectId, Common.ObjectTypeCode.Divison);
            else
                department = DataAccess.DepartmentDAL.GetDepartments(null, null);

            Session["ObjectId"] = null;

            BindCompanyList();

            return View(department);
        }

        [HttpPost]
        public ActionResult DepartmentResult(string company)
        {
            if(!string.IsNullOrEmpty(company))
            {
            // TODO: Add insert logic here
                int objectId = Convert.ToInt32(company);
                string objectTypeCode = Common.ObjectTypeCode.Company;
                return RedirectToAction("CreateDepartment", "Department", new { objectId, objectTypeCode });
            }
            else
            {
                List<DepartmentModel> department = DataAccess.DepartmentDAL.GetDepartments(null,null);

                BindCompanyList();

                return View(department);
            }

        }

        void BindCompanyList()
        {
            string addedFor = Session["UserEmailId"].ToString();
            List<CompanyModel> compaines = CompanyDAL.GetCompanies(addedFor);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (CompanyModel c in compaines)
            {
                items.Add(new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() });
            }
            ViewBag.Company = items;
        }

        //
        // GET: /Department/Details/5

        public ActionResult DepartmentDetails(int departmentId)
        {
            DepartmentModel department = new DepartmentModel();
            department = DataAccess.DepartmentDAL.Get(departmentId);
            return View(department);
        }

        //
        // GET: /Department/Create

        public ActionResult CreateDepartment()
        {
            return View();
        } 

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult CreateDepartment(DepartmentModel department,int objectId,string objectTypeCode)
        {
            department.ObjectId = objectId;
            department.ObjectTypeCode = objectTypeCode;
            department.AddedBy = User.Identity.Name;
            DataAccess.DepartmentDAL.Insert(department);

            return RedirectToAction("DepartmentResult");
        }
        
        //
        // GET: /Department/Edit/5
 
        public ActionResult EditDepartment(int departmentId)
        {
            DepartmentModel department = new DepartmentModel();
            department = DataAccess.DepartmentDAL.Get(departmentId);
            return View(department);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult EditDepartment(int departmentId, DepartmentModel department)
        {
            department.UpdatedBy = User.Identity.Name;
            DataAccess.DepartmentDAL.Update(department);

            return RedirectToAction("DepartmentResult");
        }

        //
        // GET: /Department/Delete/5

        public ActionResult DeleteDepartment(int departmentId)
        {
            DataAccess.DepartmentDAL.Delete(departmentId);
            return RedirectToAction("DepartmentResult");
        }

        public ActionResult DepartmentUsers(int departmentId)
        {
            Session["ObjectId"] = departmentId;
            return RedirectToAction("DepartmentUsersResult", "DepartmentUser");
        }

        
    }
}
