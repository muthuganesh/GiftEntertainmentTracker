using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSQ.GiftEntertainmentTracker.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.DataAccess;
using RSQ.GiftEntertainmentTracker.Security;
using RSQ.GiftEntertainmentTracker.Common;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult CompanyResult(int? companyId)
        {
            string[] role = Roles.GetRolesForUser(User.Identity.Name);
            if (role.Count() > 0)
            {
                foreach (string r in role)
                {
                    var rolePermissions = RolePremissionSetDAL.GetRolePermissionSets(r);
                    if (rolePermissions != null)
                    {
                        CompanyPermission cmp = new CompanyPermission(rolePermissions);

                        if (cmp.CanICreateCompany)
                            ViewBag.CompanyCreate = cmp.CanICreateCompany;

                        if (cmp.CanIEditCompany)
                            ViewData["CompanyEdit"] = string.Format(Common.ListItem.edit);
                        else
                            ViewData["CompanyEdit"] = string.Format(Common.ListItem.nullValue);

                        if (cmp.CanIDeleteCompany)
                            ViewData["CompanyDelete"] = string.Format(Common.ListItem.delete);
                        else
                            ViewData["CompanyDelete"] = string.Format(Common.ListItem.nullValue);

                        if (cmp.CanIDivDepCompany)
                            ViewData["CompanyDivDep"] = string.Format(Common.ListItem.divDep);
                        else
                            ViewData["CompanyDivDep"] = string.Format(Common.ListItem.nullValue);
                    }
                }
            }
            else
            {
                ViewData["CompanyEdit"] = string.Format(Common.ListItem.edit);
                ViewData["CompanyDelete"] = string.Format(Common.ListItem.delete);
                ViewData["CompanyDivDep"] = string.Format(Common.ListItem.divDep);
            }

            CompanyModel company=new CompanyModel();
            List<CompanyModel> companies=new List<CompanyModel>();
            if (companyId.HasValue)
            {
                company = DataAccess.CompanyDAL.Get(Convert.ToInt32(companyId));
                companies.Add(company);
            }
            else
                companies = DataAccess.CompanyDAL.GetCompanies();

            return View(companies);
        }

        //
        // GET: /Company/Details/5

        public ActionResult CompanyDetails(int companyId)
        {
            CompanyModel company = new CompanyModel();
            company = DataAccess.CompanyDAL.Get(companyId);
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult CreateCompany()
        {
            return View();
        } 

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult CreateCompany(CompanyModel company)
        {
                // TODO: Add insert logic here
            company.AddedBy = User.Identity.Name;
            DataAccess.CompanyDAL.Insert(company);

            //string userName = User.Identity.Name;

            //string roleName = "Super Admin";
            //if (Roles.RoleExists(roleName))
            //{
            //    AddUserToRoles(userName, roleName);
            //}
            //else
            //{
            //    Roles.CreateRole(roleName);
            //    AddUserToRoles(userName, roleName);
            //}
            if (company.CompanyId != 0)
            {
                string url = string.Format("http://23.21.244.58/Company/CompanyResult?companyId={0}&roleName={1}", company.CompanyId,"Super Admin");
                SuperAdminGenerator.Mail(company.EmailId.Trim(), url.Trim());
            }
            return RedirectToAction("CompanyResult", new { companyId = company.CompanyId });
        }

        //private void AddUserToRoles(string userName, string roleName)
        //{
        //    string[] roles = null;
        //    if (userName != null)
        //    {
        //        roles = Roles.GetRolesForUser(userName);
        //        if (roles != null && roles.Length > 0)
        //            Roles.RemoveUserFromRoles(userName, roles);
        //    }
        //    Roles.AddUserToRole(userName, roleName);
        //}
        
        //
        // GET: /Company/Edit/5
        
        [HttpGet]
        public ActionResult EditCompany(int companyId)
        {
            CompanyModel company = new CompanyModel();
            company = DataAccess.CompanyDAL.Get(companyId);
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult EditCompany(int companyId, CompanyModel company)
        {

            // TODO: Add update logic here
            company.UpdatedBy = User.Identity.Name;
            DataAccess.CompanyDAL.Update(company);

            return RedirectToAction("CompanyResult", new { companyId = company.CompanyId });
        }

        //
        // GET: /Company/Delete/5

        public ActionResult DeleteCompany(int companyId)
        {
            List<DepartmentModel> departments = DataAccess.DepartmentDAL.GetDepartments(companyId,Common.ObjectTypeCode.Company);

            foreach (var d in departments)
            {
                DataAccess.DepartmentDAL.Delete(d.DepartmentId);
            }

            foreach (var d in departments)
            {
                DataAccess.DivisionDAL.Delete(d.DivisionId);
            }

            departments = DataAccess.DepartmentDAL.GetDepartments(companyId, Common.ObjectTypeCode.Company);
            if (departments.Count == 0)
            {
                DataAccess.CompanyDAL.Delete(companyId);
                return RedirectToAction("CompanyResult", new { companyId = DBNull.Value });
            }

            return RedirectToAction("CompanyResult", new { companyId = companyId });
        }

        public ActionResult Select(int companyId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = Common.ListItem.division, Value = Common.ObjectTypeCode.Divison });
            items.Add(new SelectListItem { Text = Common.ListItem.department, Value = Common.ObjectTypeCode.Department });
            ViewBag.companyItems = items;
            Session["companyId"]=companyId;
            return PartialView("Select");
                //RedirectToAction("CreateDivision", "Division", new { objectId = companyId, objectTypeCode = Common.ObjectTypeCode.Company });
        }

        [HttpPost]
        public ActionResult Select(string companyItems)
        {
            int companyId = Convert.ToInt32(Session["companyId"]);
            string action=null, route = null;
            if (companyItems == Common.ObjectTypeCode.Divison)
            {
                action="CreateDivision";
                route="Division";
                goto send;
            }
            else if (companyItems == Common.ObjectTypeCode.Department)
            {
                action="CreateDepartment";
                route="Department";
                goto send;
            }
            send:
            return RedirectToAction(action, route, new { objectId = companyId, objectTypeCode = Common.ObjectTypeCode.Company });
        }
    }
}
