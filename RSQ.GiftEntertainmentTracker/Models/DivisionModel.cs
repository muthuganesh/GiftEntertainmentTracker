﻿using System;
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

        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Division Name")]
        [DataType(DataType.Text)]
        public string DivisionName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string AddressLine1 { get; set; }

        [DataType(DataType.Text)]
        public string AddressLine2 { get; set; }

        [DataType(DataType.Text)]
        public string AddressLine3 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ZipCode { get; set; }

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