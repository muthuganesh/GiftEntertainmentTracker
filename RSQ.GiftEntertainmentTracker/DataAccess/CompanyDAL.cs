using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Web.Security;

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
                            AddressLine1,
                            AddressLine2,
                            AddressLine3,
                            State,
                            Country,
                            ZipCode,
                            Phone,
                            EmailId,
                            Fax,
                            AddedFor
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
                    company.AddressLine1 = dr["AddressLine1"].ToString().Trim();
                    company.AddressLine2 = dr["AddressLine2"].ToString().Trim();
                    company.AddressLine3 = dr["AddressLine3"].ToString().Trim();
                    company.State = dr["State"].ToString().Trim();
                    company.Country = dr["Country"].ToString().Trim();
                    company.ZipCode = dr["ZipCode"].ToString().Trim();
                    company.PhoneNo = dr["Phone"].ToString().Trim();
                    company.EmailId = dr["EmailId"].ToString().Trim();
                    company.FaxNo = dr["Fax"].ToString().Trim();
                    company.AddedFor = dr["AddedFor"].ToString().Trim();
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
                command.Parameters.AddWithValue(@"tAddressLine1", company.AddressLine1);
                command.Parameters.AddWithValue(@"tAddressLine2", company.AddressLine2);
                command.Parameters.AddWithValue(@"tAddressLine3", company.AddressLine3);
                command.Parameters.AddWithValue(@"tState", company.State);
                command.Parameters.AddWithValue(@"tCountry", company.Country);
                command.Parameters.AddWithValue(@"tZipCode", company.ZipCode);
                command.Parameters.AddWithValue(@"tPhone", company.PhoneNo);
                command.Parameters.AddWithValue(@"tEmailId", company.EmailId);
                command.Parameters.AddWithValue(@"tFax", company.FaxNo);
                command.Parameters.AddWithValue(@"tAddedBy", company.AddedBy);

                MySqlParameter parameter = new MySqlParameter(@"tCompanyId", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(parameter);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                company.CompanyId= Convert.ToInt32(parameter.Value);
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
                command.Parameters.AddWithValue(@"tAddressLine1", company.AddressLine1);
                command.Parameters.AddWithValue(@"tAddressLine2", company.AddressLine2);
                command.Parameters.AddWithValue(@"tAddressLine3", company.AddressLine3);
                command.Parameters.AddWithValue(@"tState", company.State);
                command.Parameters.AddWithValue(@"tCountry", company.Country);
                command.Parameters.AddWithValue(@"tZipCode", company.ZipCode);
                command.Parameters.AddWithValue(@"tPhone", company.PhoneNo);
                command.Parameters.AddWithValue(@"tEmailId", company.EmailId);
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

        public static List<CompanyModel> GetCompanies(string addedFor)
        {
            DataTable table = new DataTable();
            StringBuilder codeBuilder = new StringBuilder();
            List<CompanyModel> companies = new List<CompanyModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                        SELECT
                            *
                        FROM
                        (
                        SELECT
                            CompanyId,
                            CompanyName,
                            AddressLine1,
                            AddressLine2,
                            AddressLine3,
                            State,
                            Country,
                            ZipCode,
                            Phone,
                            EmailId,
                            Fax,
                            AddedFor,
                            AddedBy
                        FROM
                            tCompany
                        WHERE
                            <<<includeSql>>>) X");
                if(!string.IsNullOrEmpty(addedFor.Trim()))
                    codeBuilder=codeBuilder.AppendFormat("AddedFor='{0}'",addedFor);
                else
                    codeBuilder=codeBuilder.AppendFormat("AddedBy='{0}'",HttpContext.Current.User.Identity.Name);

                if (!string.IsNullOrEmpty(codeBuilder.ToString().Trim()))
                    query = query.Replace("<<<includeSql>>>", codeBuilder.ToString().Trim());
                else
                    query = query.Replace("WHERE <<<includeSql>>>", codeBuilder.ToString().Trim());

                query = query.ToString().Trim();

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
                        AddressLine1 = dr["AddressLine1"].ToString().Trim(),
                        AddressLine2 = dr["AddressLine2"].ToString().Trim(),
                        AddressLine3 = dr["AddressLine3"].ToString().Trim(),
                        State=dr["State"].ToString().Trim(),
                        Country=dr["Country"].ToString().Trim(),
                        ZipCode = dr["ZipCode"].ToString().Trim(),
                        PhoneNo = dr["Phone"].ToString().Trim(),
                        EmailId = dr["EmailId"].ToString().Trim(),
                        FaxNo = dr["Fax"].ToString().Trim(),
                        AddedFor = dr["AddedFor"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim()
                    };
                    companies.Add(company);
                }
            }
            return companies;
        }
    }
}