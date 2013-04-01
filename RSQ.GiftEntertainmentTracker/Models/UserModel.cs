using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int? DivisionId { get; set; }

        public string DivisionName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        
        [Display(Name = "AddressLine1")]
        [DataType(DataType.Text)]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2")]
        [DataType(DataType.Text)]
        public string AddressLine2 { get; set; }

        [Display(Name="State")]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [Display(Name="Country")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Display(Name="ZipCode")]
        [DataType(DataType.Text)]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.Text)]
        public string PhoneNo { get; set; }

        [Required]
        [Display(Name = "Email Id")]
        [DataType(DataType.Text)]
        public string EmailId { get; set; }
        

        [Required]
        public int ObjectId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ObjectTypeCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string AddedBy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string UpdatedBy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }

    }
}