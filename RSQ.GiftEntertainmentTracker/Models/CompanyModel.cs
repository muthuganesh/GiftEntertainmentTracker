using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class CompanyModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.Text)]
        public string PhoneNo { get; set; }

        [Display(Name = "Fax Number")]
        [DataType(DataType.Text)]
        public string FaxNo { get; set; }

        [Required]
        public string AddedBy { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }
}