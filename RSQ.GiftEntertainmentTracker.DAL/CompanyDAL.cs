using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSQ.GiftEntertainmentTracker.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace RSQ.GiftEntertainmentTracker.DAL
{
    public class CompanyDAL
    {
        public static void Insert(CompanyModel company)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tCompanyInsert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                command.Parameters.AddWithValue("@Address", company.Address);
                command.Parameters.AddWithValue("@Phone", company.PhoneNo);
                command.Parameters.AddWithValue("@Fax", company.FaxNo);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
