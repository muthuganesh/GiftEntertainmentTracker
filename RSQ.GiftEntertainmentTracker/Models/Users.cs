using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class Users
    {
        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "AddressLine1")]
        [DataType(DataType.Text)]
        public string AddressLine1 { get; set; }

        [Required]
        [Display(Name = "AddressLine2")]
        [DataType(DataType.Text)]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "City")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [Required]
        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required]
        [Display(Name = "ZipCode")]
        [DataType(DataType.Text)]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.Text)]
        public string PhoneNo { get; set; }

    }
}