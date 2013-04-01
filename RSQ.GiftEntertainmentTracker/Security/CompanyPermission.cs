using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;

namespace RSQ.GiftEntertainmentTracker.Security
{
    public enum CompanyPermissionSet
    {
        None = 0,
        Create = 1,
        Edit = 2,
        Delete = 4,
        DivDep = 8,
    }
    public class CompanyPermission
    {
        private CompanyPermissionSet _pset = 0;
        public CompanyPermission(List<RolePermissionSetModel> permissionSets)
        {
            var ps = permissionSets.Where(p => p.PermissionSetType == "RSQ.GiftEntertainmentTracker.Security.CompanyPermissionSet").FirstOrDefault();
            if (ps != null)
                _pset = (CompanyPermissionSet)Enum.Parse(typeof(CompanyPermissionSet), ps.PermissionSet.ToString());
        }
        public bool CanICreateCompany
        {
            get
            {
                return (CompanyPermissionSet.Create == (_pset & CompanyPermissionSet.Create));
            }
        }
        public bool CanIEditCompany
        {
            get
            {
                return (CompanyPermissionSet.Edit == (_pset & CompanyPermissionSet.Edit));
            }
        }
        public bool CanIDeleteCompany
        {
            get
            {
                return (CompanyPermissionSet.Delete == (_pset & CompanyPermissionSet.Delete));
            }
        }
        public bool CanIDivDepCompany
        {
            get
            {
                return (CompanyPermissionSet.DivDep == (_pset & CompanyPermissionSet.DivDep));
            }
        }
    }
}