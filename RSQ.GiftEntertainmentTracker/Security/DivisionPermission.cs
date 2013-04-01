using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;

namespace RSQ.GiftEntertainmentTracker.Security
{
    public enum DivisionPermissionSet
    {
        None = 0,
        Create = 1,
        Edit = 2,
        Delete = 4,
        Department = 8,
    }

    public class DivisionPermission
    {
        private DivisionPermissionSet _pset = 0;
        public DivisionPermission(List<RolePermissionSetModel> permissionSets)
        {
            var ps = permissionSets.Where(p => p.PermissionSetType == "RSQ.GiftEntertainmentTracker.Security.DivisionPermissionSet").FirstOrDefault();
            if (ps != null)
                _pset = (DivisionPermissionSet)Enum.Parse(typeof(DivisionPermissionSet), ps.PermissionSet.ToString());
        }
        public bool CanICreateDivision
        {
            get
            {
                return (DivisionPermissionSet.Create == (_pset & DivisionPermissionSet.Create));
            }
        }
        public bool CanIEditDivision
        {
            get
            {
                return (DivisionPermissionSet.Edit == (_pset & DivisionPermissionSet.Edit));
            }
        }
        public bool CanIDeleteDivision
        {
            get
            {
                return (DivisionPermissionSet.Delete == (_pset & DivisionPermissionSet.Delete));
            }
        }
        public bool CanIDepartmentDivision
        {
            get
            {
                return (DivisionPermissionSet.Department == (_pset & DivisionPermissionSet.Department));
            }
        }
    }
}