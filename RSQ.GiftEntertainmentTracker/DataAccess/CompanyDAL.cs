using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace RSQ.GiftEntertainmentTracker.DataAccess
{
    public class CompanyDAL
    {
        public static CompanyModel Get(int companyId)
        {
            DataTable dt = new DataTable();
            CompanyModel company = new CompanyModel();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string command = string.Format(@"
                    SELECT
                            CompanyId,
                            CompanyName,
                            Address,
                            Phone,
                            Fax
                    FROM
                        tCompany
                    WHERE
                    CompanyId={0}", companyId);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    company.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                    company.CompanyName = dr["CompanyName"].ToString().Trim();
                    company.Address = dr["Address"].ToString().Trim();
                    company.PhoneNo = dr["Phone"].ToString().Trim();
                    company.FaxNo = dr["Fax"].ToString().Trim();
                }
            }
            return company;
        }

        public static void Insert(CompanyModel company)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tCompanyInsert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tCompanyName", company.CompanyName);
                command.Parameters.AddWithValue(@"tAddress", company.Address);
                command.Parameters.AddWithValue(@"tPhone", company.PhoneNo);
                command.Parameters.AddWithValue(@"tFax", company.FaxNo);
                command.Parameters.AddWithValue(@"tAddedBy", company.AddedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update(CompanyModel company)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tCompanyUpdate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tCompanyId", company.CompanyId);
                command.Parameters.AddWithValue(@"tCompanyName", company.CompanyName);
                command.Parameters.AddWithValue(@"tAddress", company.Address);
                command.Parameters.AddWithValue(@"tPhone", company.PhoneNo);
                command.Parameters.AddWithValue(@"tFax", company.FaxNo);
                command.Parameters.AddWithValue(@"tUpdatedBy", company.UpdatedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Delete(int companyId)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tCompanyDelete", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tCompanyId", companyId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<CompanyModel> GetCompanies()
        {
            DataTable table = new DataTable();
            StringBuilder codeBuilder = new StringBuilder();
            List<CompanyModel> companies = new List<CompanyModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                        SELECT
                            CompanyId,
                            CompanyName,
                            Address,
                            Phone,
                            Fax,
                            AddedBy
                        FROM
                            tCompany");

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    CompanyModel company = new CompanyModel
                    {
                        CompanyId = Convert.ToInt32(dr["CompanyId"]),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        Address = dr["Address"].ToString().Trim(),
                        PhoneNo = dr["Phone"].ToString().Trim(),
                        FaxNo = dr["Fax"].ToString().Trim(),
                        AddedBy =dr["AddedBy"].ToString().Trim()
                    };
                    companies.Add(company);
                }
            }
            return companies;
        }
    }
}