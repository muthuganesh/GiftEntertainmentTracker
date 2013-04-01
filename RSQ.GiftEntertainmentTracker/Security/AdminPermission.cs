using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;

namespace RSQ.GiftEntertainmentTracker.Security
{
    public enum AdminPermissionSet
    {
        None = 0,
        Create = 1,
        Edit = 2,
        Delete = 4,
        ResetPassword = 8,
    }

    public class AdminPermission
    {
        private AdminPermissionSet _pset = 0;
        public AdminPermission(List<RolePermissionSetModel> permissionSets)
        {
            var ps = permissionSets.Where(p => p.PermissionSetType == "RSQ.GiftEntertainmentTracker.Security.AdminPermissionSet").FirstOrDefault();
            if (ps != null)
                _pset = (AdminPermissionSet)Enum.Parse(typeof(AdminPermissionSet), ps.PermissionSet.ToString());
        }
        public bool CanICreateUser
        {
            get
            {
                return (AdminPermissionSet.Create == (_pset & AdminPermissionSet.Create));
            }
        }
        public bool CanIEditUser
        {
            get
            {
                return (AdminPermissionSet.Edit == (_pset & AdminPermissionSet.Edit));
            }
        }
        public bool CanIDeleteUser
        {
            get
            {
                return (AdminPermissionSet.Delete == (_pset & AdminPermissionSet.Delete));
            }
        }
        public bool CanIResetPasswordUser
        {
            get
            {
                return (AdminPermissionSet.ResetPassword == (_pset & AdminPermissionSet.ResetPassword));
            }
        }
    }
}