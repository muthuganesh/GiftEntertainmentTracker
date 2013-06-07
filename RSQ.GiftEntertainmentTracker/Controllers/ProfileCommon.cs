using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace RSQ.GiftEntertainmentTracker.Controllers
{
    public class ProfileCommon : ProfileBase
    {

        public virtual string FirstName
        {
            get
            {
                return ((string)(this.GetPropertyValue("FirstName")));
            }
            set
            {
                this.SetPropertyValue("FirstName", value);
            }

        }

        public virtual string LastName
        {
            get
            {
                return ((string)(this.GetPropertyValue("LastName")));
            }
            set
            {
                this.SetPropertyValue("LastName", value);
            }
        }

        public virtual string AddressLine1
        {
            get
            {
                return ((string)(this.GetPropertyValue("AddressLine1")));
            }
            set
            {
                this.SetPropertyValue("AddressLine1", value);
            }
        }

        public virtual string AddressLine2
        {
            get
            {
                return ((string)(this.GetPropertyValue("AddressLine2")));
            }
            set
            {
                this.SetPropertyValue("AddressLine2", value);
            }
        }

        public virtual string City
        {
            get
            {
                return ((string)(this.GetPropertyValue("City")));
            }
            set
            {
                this.SetPropertyValue("City", value);
            }
        }

        public virtual string State
        {
            get
            {
                return ((string)(this.GetPropertyValue("State")));
            }
            set
            {
                this.SetPropertyValue("State", value);
            }
        }

        public virtual string Country
        {
            get
            {
                return ((string)(this.GetPropertyValue("Country")));
            }
            set
            {
                this.SetPropertyValue("Country", value);
            }
        }


        public virtual string PhoneNo
        {
            get
            {
                return ((string)(this.GetPropertyValue("PhoneNo")));
            }
            set
            {
                this.SetPropertyValue("PhoneNo", value);
            }
        }

        public virtual string ZipCode
        {
            get
            {
                return ((string)(this.GetPropertyValue("ZipCode")));
            }
            set
            {
                this.SetPropertyValue("ZipCode", value);
            }
        }

        public virtual ProfileCommon GetProfile(string username)
        {
            return Create(username) as ProfileCommon;
        }
    }

}