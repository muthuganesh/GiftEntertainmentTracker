using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSQ.GiftEntertainmentTracker.Models;
using RSQ.GiftEntertainmentTracker.Common;
using RSQ.GiftEntertainmentTracker.DataAccess;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.Security;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    [Authorize]
    public class DivisionController : Controller
    {
        //
        // GET: /Division/
        public ActionResult DivisionResult()
        {
            string[] role = Roles.GetRolesForUser(User.Identity.Name);
            if (role.Count() > 0)
            {
                foreach (string r in role)
                {
                    var rolePermissions = RolePremissionSetDAL.GetRolePermissionSets(r);
                    if (rolePermissions != null)
                    {
                        DivisionPermission div = new DivisionPermission(rolePermissions);

                        if (div.CanICreateDivision)
                            ViewBag.DivisionCreate = div.CanICreateDivision;

                        if (div.CanIEditDivision)
                            ViewData["DivisionEdit"] = string.Format(Common.ListItem.edit);
                        else
                            ViewData["DivisionEdit"] = string.Format(Common.ListItem.nullValue);

                        if (div.CanIDeleteDivision)
                            ViewData["DivisionDelete"] = string.Format(Common.ListItem.delete);
                        else
                            ViewData["DivisionDelete"] = string.Format(Common.ListItem.nullValue);

                        if (div.CanIDepartmentDivision)
                            ViewData["DivisionDepartment"] = string.Format(Common.ListItem.department);
                        else
                            ViewData["DivisionDepartment"] = string.Format(Common.ListItem.nullValue);
                    }
                }
            }
            else
            {
                ViewData["DivisionEdit"] = string.Format(Common.ListItem.edit);
                ViewData["DivisionDelete"] = string.Format(Common.ListItem.delete);
                ViewData["DivisionDepartment"] = string.Format(Common.ListItem.department);
            }

            List<DivisionModel> divisions = DataAccess.DivisionDAL.GetDivisions();

            List<CompanyModel> compaines = CompanyDAL.GetCompanies();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (CompanyModel c in compaines)
            {
                items.Add(new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() });
            }
            ViewBag.Company = items;
      
            return View(divisions);
        }

        [HttpPost]
        public ActionResult DivisionResult(string company)
        {
            
            // TODO: Add insert logic here
            if (!string.IsNullOrEmpty(company))
            {
                int objectId = Convert.ToInt32(company);
                string objectTypeCode = Common.ObjectTypeCode.Company;
                return RedirectToAction("CreateDivision", "Division", new { objectId, objectTypeCode });
            }
            else
            {
                List<DivisionModel> divisions = DataAccess.DivisionDAL.GetDivisions();

                List<CompanyModel> compaines = CompanyDAL.GetCompanies();
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (CompanyModel c in compaines)
                {
                    items.Add(new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() });
                }
                ViewBag.Company = items;
                return View(divisions);
            }
        }

        //
        // GET: /Division/Details/5

        public ActionResult DivisionDetails(int divisionId)
        {
            DivisionModel division = new DivisionModel();
            division = DataAccess.DivisionDAL.Get(divisionId);
            return View(division);
        }

        //
        // GET: /Division/Create

        public ActionResult CreateDivision()
        {
            return View();
        } 

        //
        // POST: /Division/Create

        [HttpPost]
        public ActionResult CreateDivision(DivisionModel division, int objectId,string objectTypeCode)
        {

            // TODO: Add insert logic here
            division.ObjectId = objectId;
            division.ObjectTypeCode = ObjectTypeCode.Company;
            division.AddedBy = User.Identity.Name;
            DataAccess.DivisionDAL.Insert(division);
            return RedirectToAction("DivisionResult");
        }
        
        //
        // GET: /Division/Edit/5

        public ActionResult EditDivision(int divisionId)
        {
            DivisionModel division = new DivisionModel();
            division = DataAccess.DivisionDAL.Get(divisionId);
            return View(division);
        }

        //
        // POST: /Division/Edit/5

        [HttpPost]
        public ActionResult EditDivision(int divisionId, DivisionModel division)
        {
            division.UpdatedBy = User.Identity.Name;
            DataAccess.DivisionDAL.Update(division);
            return RedirectToAction("DivisionResult");
        }

        //
        // GET: /Division/Delete/5

        public ActionResult DeleteDivision(int divisionId)
        {
            List<DepartmentModel> departments = DataAccess.DepartmentDAL.GetDepartments(divisionId, Common.ObjectTypeCode.Divison);
            foreach (var d in departments)
            {
                DepartmentDAL.Delete(d.DepartmentId);
            }

            departments = DataAccess.DepartmentDAL.GetDepartments(divisionId, Common.ObjectTypeCode.Divison);
            if (departments.Count == 0)
            {
                DataAccess.DivisionDAL.Delete(divisionId);
            }
            return RedirectToAction("DivisionResult");
        }

        public ActionResult Select(int divisionId)
        {
            return RedirectToAction("CreateDepartment", "Department", new { objectId = divisionId, objectTypeCode = Common.ObjectTypeCode.Divison });
        }
    }
}
