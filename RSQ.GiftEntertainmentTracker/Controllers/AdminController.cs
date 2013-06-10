using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using Rsq.UserProfileManagement.Models;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.Models;
using System.Web.Mail;
using System.Net.Mail;
using RSQ.GiftEntertainmentTracker.Controllers;
using RSQ.GiftEntertainmentTracker.Common;
using RSQ.GiftEntertainmentTracker.DataAccess;
using RSQ.GiftEntertainmentTracker.Security;
using System.Web.Profile;

namespace Rsq.UserProfileManagement.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            BindUserPermission();

            List<MembershipModel> members = new List<MembershipModel>();
            foreach (MembershipUser m in Membership.GetAllUsers())
            {
                MembershipModel member = new MembershipModel
                {
                    UserName = m.UserName.ToString(),
                    Email = m.Email.ToString(),
                    Comment = m.Comment.ToString()
                };
                members.Add(member);
            }
            BindUserList();
            return View(members);
        }

        private void BindUserPermission()
        {
            string[] role = Roles.GetRolesForUser(User.Identity.Name);
            if (role.Count() > 0)
            {
                foreach (string r in role)
                {
                    var rolePermissions = RolePremissionSetDAL.GetRolePermissionSets(r);
                    if (rolePermissions != null)
                    {
                        AdminPermission ad = new AdminPermission(rolePermissions);

                        if (ad.CanICreateUser)
                            ViewBag.UserCreate = ad.CanICreateUser;

                        if (ad.CanIEditUser)
                            ViewData["UserEdit"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.edit);
                        else
                            ViewData["UserEdit"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.nullValue);

                        if (ad.CanIDeleteUser)
                            ViewData["UserDelete"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.delete);
                        else
                            ViewData["UserDelete"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.nullValue);

                        if (ad.CanIResetPasswordUser)
                            ViewData["UserResetPassword"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.resetPassword);
                        else
                            ViewData["UserResetPassword"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.nullValue);
                    }
                }
            }
            else
            {
                ViewData["UserEdit"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.edit);
                ViewData["UserDelete"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.delete);
                ViewData["UserResetPassword"] = string.Format(RSQ.GiftEntertainmentTracker.Common.ListItem.resetPassword);
            }
        }

        [HttpPost]
        public ActionResult Index(string userName)
        {
            MembershipUser user;
            MembershipModel member = new MembershipModel();
            List<MembershipModel> members = new List<MembershipModel>();
            if (userName != null)
            {
                user = Membership.GetUser(userName);
                if (user != null)
                {
                    member.UserName = user.UserName;
                    member.Email = user.Email;
                    member.Comment = user.Comment;
                    member.IsApproved = user.IsApproved;
                    member.IsLockedOut = user.IsLockedOut;
                    member.IsOnline = user.IsOnline;
                    members.Add(member);
                }
            }

            BindUserPermission();
            BindUserList();
            return View(members);
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
        //
        // GET: /Admin/Details/5

        public ActionResult Details(string userName)
        {
            return RedirectToAction("UserDetails", "Profile", new { userName = userName });
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Create

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
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(string userName)
        {
            MembershipUser user;
            MembershipModel member=new MembershipModel();
            if (userName != null)
            {
                user = Membership.GetUser(userName);
                member.UserName = user.UserName;
                member.Email = user.Email;
                member.Comment = user.Comment;
            }
            return View(member);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(MembershipModel member, FormCollection collection)
        {
            MembershipUser user;
            try
            {
                if (member.UserName != null)
                {
                    user = Membership.GetUser(member.UserName.ToString().Trim());
                    user.Email = member.Email.ToString().Trim();
                    user.Comment = member.Comment.ToString().Trim();
                    Membership.UpdateUser(user);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(MembershipModel member)
        {
            string userName = member.UserName.ToString().Trim();
            if (!string.IsNullOrEmpty(userName) && member.IsOnline==false)
            {
                ProfileManager.DeleteProfile(userName);
                Membership.DeleteUser(userName);
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(MembershipModel member, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/RessetPassword/5

        public ActionResult ResetPassword(string userName)
        {
            MembershipUser user;
            MembershipModel member = new MembershipModel();
            if (userName != null)
            {
                user = Membership.GetUser(userName);
                member.Email = user.Email;
            }
            return View(member);
        }

        [HttpPost]
        public ActionResult ResetPassword(string userName, FormCollection collection)
        {
            MembershipUser user,currentUser;
            string password = Membership.GeneratePassword(6, 0);
            if (userName != null)
            {
                user = Membership.GetUser(userName.ToString().Trim());
                user.ChangePassword(user.GetPassword(), password);
                Membership.UpdateUser(user);
                currentUser = Membership.GetUser(User.Identity.Name);

                MailGenerator.Mail(user.Email.ToString(), password);
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult Active(string userName, FormCollection collection)
        //{
        //    //MembershipUser user;
        //    //foreach (GridViewRow row in grdUserList.Rows)
        //    //{
        //    //    CheckBox cb = (CheckBox)row.FindControl("chkUser");
        //    //    Label lblUser = (Label)row.FindControl("lblUserName");
        //    //    if (cb.Checked)
        //    //    {
        //    //        user = Membership.GetUser(lblUser.Text);
        //    //        user.IsApproved = true;
        //    //        Membership.UpdateUser(user);
        //    //    }
        //    //}
        //    //return RedirectToAction("Index");
        //}
    }
}
