using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class DivisionModel
    {
        public int DivisionId { get; set; }

        //[Required(ErrorMessage = "Please select a Country")]
        //public int CompanyId { get; set; }


        //public IEnumerable<SelectListItem> CompanyList
        //{
        //    get
        //    {
        //        return DataAccess.CompanyDAL.GetCompanies()
        //            .Select(company => new SelectListItem
        //            {
        //                Text = company.CompanyName,
        //                Value = company.CompanyId.ToString()
        //            })
        //            .ToList();
        //    }
        //}

        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Division Name")]
        [DataType(DataType.Text)]
        public string DivisionName { get; set; }

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
        [HiddenInput(DisplayValue = false)]
        public int ObjectId { get; set; }

        [Required]
        [HiddenInput(DisplayValue=false)]
        [DataType(DataType.Text)]
        public string ObjectTypeCode { get; set; }

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