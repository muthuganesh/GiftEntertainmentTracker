using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        [DataType(DataType.Text)]
        public string DepartmentName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.Text)]
        public string PhoneNo { get; set; }

        [Display(Name = "Fax Number")]
        [DataType(DataType.Text)]
        public string FaxNo { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int ObjectId { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Text)]
        public string ObjectTypeCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string AddedBy { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

    }
}