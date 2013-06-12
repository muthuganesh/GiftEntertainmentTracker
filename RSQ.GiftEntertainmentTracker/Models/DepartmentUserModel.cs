using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class DepartmentUserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int ObjectId { get; set; }

        public string ObjectTypeCode { get; set; }

        public string AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string CompanyName { get; set; }

        public string DivisionName { get; set; }

        public string DepartmentName { get; set; }

    }
}