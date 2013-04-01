using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;


namespace RSQ.GiftEntertainmentTracker.Common
{
    public class ConnectionString
    {
       public static readonly string GiftDb = ConfigurationManager.ConnectionStrings["GiftDb"].ConnectionString;
    }
}
