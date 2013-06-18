using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using RSQ.GiftEntertainmentTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mail;
using System.Net.Mail;
using RSQ.GiftEntertainmentTracker.Common;



namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn(string returnUrl)
        {
            Session["ReturnUrl"] = returnUrl;
            if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
                return RedirectToAction("LogOff", "Account");
            else
                return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel  model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                foreach (MembershipUser user in Membership.FindUsersByEmail(model.Email))
                {
                    if (Membership.ValidateUser(user.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, model.RememberMe);
                        
                        string roleName = Session["roleName"] as string;
                        string[] roles = null;
                        if (!string.IsNullOrEmpty(roleName) && Roles.RoleExists(roleName))
                        {
                            AddUserToRole(user, roleName, roles);
                        }
                        else if (!string.IsNullOrEmpty(roleName))
                        {
                            Roles.CreateRole(roleName);
                            AddUserToRole(user, roleName, roles);
                        }


                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            Session["UserEmailId"] = model.Email;
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            Session["UserEmailId"] = DBNull.Value;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        private static void AddUserToRole(MembershipUser user, string roleName, string[] roles)
        {
            roles = Roles.GetRolesForUser(user.UserName);
            if (roles != null && roles.Length > 0)
                Roles.RemoveUserFromRoles(user.UserName, roles);
            Roles.AddUserToRole(user.UserName, roleName);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn", "Account");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user 
                //UserProfileModel userprofile;
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.Name, model.Password, model.Email, null, null, true, null, out createStatus);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    string returnUrl = Session["ReturnUrl"] == null ? DBNull.Value.ToString() : Session["ReturnUrl"].ToString();
                    returnUrl = returnUrl.Trim();
                    return RedirectToAction("LogOn", new { returnUrl = returnUrl });

                    //FormsAuthentication.SetAuthCookie(model.Name, false /* createPersistentCookie */);
                    //return RedirectToAction("CreateUser", "Profile");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddUserToRoles(string userName, string roleName)
        {
            string[] roles = null;
            if (userName != null)
            {
                roles = Roles.GetRolesForUser(userName);
                if (roles != null && roles.Length > 0)
                    Roles.RemoveUserFromRoles(userName, roles);
            }
            Roles.AddUserToRole(userName, roleName);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgot)
        {
            MembershipUser user;
            string password = Membership.GeneratePassword(8, 0);

            foreach (MembershipUser u in Membership.FindUsersByEmail(forgot.Email.ToString()))
            {
                user = Membership.GetUser(u.UserName.ToString().Trim());
                user.ChangePassword(user.GetPassword(), password);
                Membership.UpdateUser(user);
            }

            MailGenerator.Mail(forgot.Email.ToString(), password);

            return RedirectToAction("ChangePasswordSuccess");
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
