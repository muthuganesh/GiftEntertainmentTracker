using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.Models;
using RSQ.GiftEntertainmentTracker.Security;
using RSQ.GiftEntertainmentTracker.DataAccess;
using Rsq.UserProfileManagement.Models;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

        public ActionResult RoleList()
        {
            string[] roles = null;
            roles = Roles.GetAllRoles();
            RoleModel role = new RoleModel
            {
                Role = roles
            };

            return View(role);
        }
        
        //
        // GET: /Role/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Role/Create

        public ActionResult CreateRole()
        {
            ViewData["Role"] = " ";
            return View();
        } 

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult CreateRole(string roleName)
        {
            if (Roles.RoleExists(roleName))
            {
                ViewData["Role"] = "<script>alert('Role Name already exist');</script>";
                return View();
            }
            else
            {
                Roles.CreateRole(roleName);
                return RedirectToAction("RoleList");
            }
        }
        
        //
        // GET: /Role/Edit/5

        public ActionResult EditRole(string roleName)
        {
            ViewData["RoleName"] = roleName;
            ViewData["Role"] = " ";
            return View();
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult EditRole(string newroleName, string roleName)
        {
            if (Roles.RoleExists(newroleName))
            {
                ViewData["Role"] = "<script>alert('Role Name already exist');</script>";
                return View();
            }
            else
            {
                Roles.CreateRole(newroleName);
                string[] users = Roles.GetUsersInRole(roleName);
                foreach (var user in users)
                {
                    Roles.AddUserToRole(user, newroleName);
                    Roles.RemoveUserFromRole(user, roleName);
                }
                Roles.DeleteRole(roleName);
                return RedirectToAction("RoleList");
            }
        }

        //
        // GET: /Role/Delete/5

        public ActionResult DeleteRole(string roleName)
        {
            Roles.DeleteRole(roleName);
            return RedirectToAction("RoleList");
        }

        public ActionResult RoleUserList(string roleName)
        {

            //string[] users = null;
            //users = Roles.GetUsersInRole(roleName);
            ViewData["RoleName"] = roleName;
            RoleModel user = new RoleModel
            {
                User = Roles.GetUsersInRole(roleName)
            };
            Session["RoleName"] = roleName;

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (MembershipUser m in Membership.GetAllUsers())
            {
                items.Add(new SelectListItem { Text = m.UserName, Value = m.UserName });
            }

            ViewBag.UserName = items;

            return View(user);
        }

        public ActionResult RemoveUser(string userName)
        {
            string roleName = Session["RoleName"] as string;
            Roles.RemoveUserFromRole(userName, roleName);

            return RedirectToAction("RoleList");
        }

        public ActionResult RolePermission(string roleName,FormCollection formCollection)
        {
            ViewData["RoleName"] = roleName;
            var rolePermissions = RolePremissionSetDAL.GetRolePermissionSets(roleName);
            if (rolePermissions != null)
            {
                CompanyPermission cmp = new CompanyPermission(rolePermissions);
             
                if (cmp.CanICreateCompany)
                    ViewBag.CompanyCreate = cmp.CanICreateCompany;
                if (cmp.CanIEditCompany)
                    ViewBag.CompanyEdit = cmp.CanIEditCompany;
                if (cmp.CanIDeleteCompany)
                    ViewBag.CompanyDelete = cmp.CanIDeleteCompany;
                if (cmp.CanIDivDepCompany)
                    ViewBag.CompanyDivDep = cmp.CanIDivDepCompany;

                DivisionPermission div = new DivisionPermission(rolePermissions);

                if (div.CanICreateDivision == true)
                    ViewBag.DivisionCreate = div.CanICreateDivision;
                if (div.CanIEditDivision == true)
                    ViewBag.DivisionEdit = div.CanIEditDivision;
                if (div.CanIDeleteDivision == true)
                    ViewBag.DivisionDelete = div.CanIDeleteDivision;
                if (div.CanIDepartmentDivision == true)
                    ViewBag.DivisionDepartment = div.CanIDepartmentDivision;

                DepartmentPermission dep = new DepartmentPermission(rolePermissions);

                if (dep.CanICreateDepartment == true)
                    ViewBag.DepartmentCreate = div.CanICreateDivision;
                if (dep.CanIEditDepartment == true)
                    ViewBag.DepartmentEdit = div.CanIEditDivision;
                if (dep.CanIDeleteDepartment == true)
                    ViewBag.DepartmentDelete = div.CanIDeleteDivision;

                AdminPermission ad = new AdminPermission(rolePermissions);

                if (ad.CanICreateUser == true)
                    ViewBag.UserCreate = ad.CanICreateUser;
                if (ad.CanIEditUser == true)
                    ViewBag.UserEdit = ad.CanIEditUser;
                if (ad.CanIDeleteUser == true)
                    ViewBag.UserDelete = ad.CanIDeleteUser;
                if (ad.CanIResetPasswordUser == true)
                    ViewBag.UserResetPassword = ad.CanIResetPasswordUser;
            }
            Session["RoleName"] = roleName;
            return View();
        }

        [HttpPost]
        public ActionResult RolePermission(FormCollection formCollection, bool companyCreate,
                                            bool companyEdit, bool companyDelete,
                                            bool companyDivDep,bool divisionCreate,
                                            bool divisionEdit,bool divisionDelete,
                                            bool divisionDepartment,bool departmentCreate,
                                            bool departmentDelete,bool departmentEdit,
                                            bool userCreate,bool userEdit,
                                            bool userDelete,bool userResetPassword)
        {
            string roleName = Session["RoleName"] as string;
            var rolePermissions = RolePremissionSetDAL.GetRolePermissionSets(roleName);
            if (rolePermissions.Count > 0 && rolePermissions != null)
                RolePremissionSetDAL.Delete(roleName);
            RolePermissionSetModel pset = new RolePermissionSetModel();

            CompanyPermissionSet comapnyPS = CompanyPermissionSet.None;

            if (companyCreate == true)
                comapnyPS |= CompanyPermissionSet.Create;
            if (companyEdit == true)
                comapnyPS |= CompanyPermissionSet.Edit;
            if (companyDelete == true)
                comapnyPS |= CompanyPermissionSet.Delete;
            if (companyDivDep == true)
                comapnyPS |= CompanyPermissionSet.DivDep;

            pset.PermissionSet = (int)comapnyPS;
            pset.PermissionSetType = typeof(CompanyPermissionSet).ToString();
            pset.RoleName = roleName;
            pset.AddedBy = User.Identity.Name;
            RolePremissionSetDAL.Insert(pset);

            DivisionPermissionSet divisionPS = DivisionPermissionSet.None;

            if (divisionCreate == true)
                divisionPS |= DivisionPermissionSet.Create;
            if (divisionEdit == true)
                divisionPS |= DivisionPermissionSet.Edit;
            if (divisionDelete == true)
                divisionPS |= DivisionPermissionSet.Delete;
            if (divisionDepartment == true)
                divisionPS |= DivisionPermissionSet.Department;

            pset.PermissionSet = (int)divisionPS;
            pset.PermissionSetType = typeof(DivisionPermissionSet).ToString();
            pset.RoleName = roleName;
            pset.AddedBy = User.Identity.Name;
            RolePremissionSetDAL.Insert(pset);

            DepartmentPermissionSet departmentPS = DepartmentPermissionSet.None;

            if (departmentCreate == true)
                departmentPS |= DepartmentPermissionSet.Create;
            if (departmentEdit == true)
                departmentPS |= DepartmentPermissionSet.Edit;
            if (departmentDelete == true)
                departmentPS |= DepartmentPermissionSet.Delete;

            pset.PermissionSet = (int)departmentPS;
            pset.PermissionSetType = typeof(DepartmentPermissionSet).ToString();
            pset.RoleName = roleName;
            pset.AddedBy = User.Identity.Name;
            RolePremissionSetDAL.Insert(pset);

            AdminPermissionSet adminPS = AdminPermissionSet.None;

            if (userCreate == true)
                adminPS |= AdminPermissionSet.Create;
            if (userEdit == true)
                adminPS |= AdminPermissionSet.Edit;
            if (userDelete == true)
                adminPS |= AdminPermissionSet.Delete;
            if (userResetPassword == true)
                adminPS |= AdminPermissionSet.ResetPassword;

            pset.PermissionSet = (int)adminPS;
            pset.PermissionSetType = typeof(AdminPermissionSet).ToString();
            pset.RoleName = roleName;
            pset.AddedBy = User.Identity.Name;
            RolePremissionSetDAL.Insert(pset);

            return RedirectToAction("RoleList");
        }

        [HttpPost]
        public ActionResult AddUserToRole(string userName)
        {
            string[] rolenames = Roles.GetRolesForUser(userName);
            string roleName = Session["RoleName"] as string;
            string[] roles = null;
            if (userName != null)
            {
                roles = Roles.GetRolesForUser(userName);
                if (roles != null && roles.Length > 0)
                    Roles.RemoveUserFromRoles(userName, roles);
            }
            Roles.AddUserToRole(userName, roleName);

            return RedirectToAction("RoleList");
        }
    }
}
