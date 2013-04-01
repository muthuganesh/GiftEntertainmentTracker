using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class RolePermissionSetModel
    {
        public string RoleName { get; set; }
        public int PermissionSet { get; set; }
        public string PermissionSetType { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}