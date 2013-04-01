using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;

namespace RSQ.GiftEntertainmentTracker.Security
{
    public enum DepartmentPermissionSet
    {
        None = 0,
        Create = 1,
        Edit = 2,
        Delete = 4
    }

    public class DepartmentPermission
    {
        private DepartmentPermissionSet _pset = 0;
        public DepartmentPermission(List<RolePermissionSetModel> permissionSets)
        {
            var ps = permissionSets.Where(p => p.PermissionSetType == "RSQ.GiftEntertainmentTracker.Security.DepartmentPermissionSet").FirstOrDefault();
            if (ps != null)
                _pset = (DepartmentPermissionSet)Enum.Parse(typeof(DepartmentPermissionSet), ps.PermissionSet.ToString());
        }
        public bool CanICreateDepartment
        {
            get
            {
                return (DepartmentPermissionSet.Create == (_pset & DepartmentPermissionSet.Create));
            }
        }
        public bool CanIEditDepartment
        {
            get
            {
                return (DepartmentPermissionSet.Edit == (_pset & DepartmentPermissionSet.Edit));
            }
        }
        public bool CanIDeleteDepartment
        {
            get
            {
                return (DepartmentPermissionSet.Delete == (_pset & DepartmentPermissionSet.Delete));
            }
        }
    }
}