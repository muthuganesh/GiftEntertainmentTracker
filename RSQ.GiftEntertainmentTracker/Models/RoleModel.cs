using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace RSQ.GiftEntertainmentTracker.Models
{
    public class RoleModel
    {
        [DisplayName("Roles")]
        public string[] Role { get; set; }

        public string[] User { get; set; }
    }
}