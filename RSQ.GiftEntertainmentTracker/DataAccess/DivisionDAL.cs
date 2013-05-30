using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace RSQ.GiftEntertainmentTracker.DataAccess
{
    public class DivisionDAL
    {
        public static DivisionModel Get(int divisionId)
        {
            DataTable dt = new DataTable();
            DivisionModel division = new DivisionModel();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string command = string.Format(@"
                   SELECT 
	                        td.DivisionId,
	                        tc.CompanyId,
	                        tc.CompanyName,
	                        td.DivisionName,
	                        td.AddressLine1,
                            td.AddressLine2,
                            td.AddressLine3,
                            td.State,
                            td.Country,
                            td.ZipCode,
	                        td.PhoneNo,
	                        td.FaxNo,
	                        td.ObjectId,
	                        td.ObjectTypeCode,
	                        td.AddedBy,
	                        td.UpdatedBy

                        FROM 
	                        tDivision as td 
                        INNER JOIN
	                        tCompany as tc
                        ON
	                        td.ObjectId=tc.CompanyId
						AND
							td.ObjectTypeCode='CM'
                    WHERE
                    td.DivisionId={0}", divisionId);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    division.DivisionId = Convert.ToInt32(dr["DivisionId"]);
                    division.CompanyName = dr["CompanyName"].ToString().Trim();
                    division.DivisionName = dr["DivisionName"].ToString().Trim();
                    division.AddressLine1 = dr["AddressLine1"].ToString().Trim();
                    division.AddressLine2 = dr["AddressLine2"].ToString().Trim();
                    division.AddressLine3 = dr["AddressLine3"].ToString().Trim();
                    division.State = dr["State"].ToString().Trim();
                    division.Country = dr["Country"].ToString().Trim();
                    division.ZipCode = dr["ZipCode"].ToString().Trim();
                    division.PhoneNo = dr["PhoneNo"].ToString().Trim();
                    division.FaxNo = dr["FaxNo"].ToString().Trim();
                    division.ObjectId = Convert.ToInt32(dr["ObjectId"]);
                    division.ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim();
                    division.AddedBy = dr["AddedBy"].ToString().Trim();
                    division.UpdatedBy = dr["UpdatedBy"].ToString().Trim();
                }
            }
            return division;
        }

        public static void Insert(DivisionModel division)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDivisionInsert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tDivisionName", division.DivisionName);
                command.Parameters.AddWithValue(@"tAddressLine1", division.AddressLine1);
                command.Parameters.AddWithValue(@"tAddressLine2", division.AddressLine2);
                command.Parameters.AddWithValue(@"tAddressLine3", division.AddressLine3);
                command.Parameters.AddWithValue(@"tState", division.State);
                command.Parameters.AddWithValue(@"tCountry", division.Country);
                command.Parameters.AddWithValue(@"tZipCode", division.ZipCode);
                command.Parameters.AddWithValue(@"tPhoneNo", division.PhoneNo);
                command.Parameters.AddWithValue(@"tFaxNo", division.FaxNo);
                command.Parameters.AddWithValue(@"tObjectId", division.ObjectId);
                command.Parameters.AddWithValue(@"tObjectTypeCode", division.ObjectTypeCode);
                command.Parameters.AddWithValue(@"tAddedBy", division.AddedBy);
                command.Parameters.AddWithValue(@"tUpdatedBy", division.UpdatedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update(DivisionModel division)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDivisionUpdate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tDivisionId", division.DivisionId);
                command.Parameters.AddWithValue(@"tDivisionName", division.DivisionName);
                command.Parameters.AddWithValue(@"tAddressLine1", division.AddressLine1);
                command.Parameters.AddWithValue(@"tAddressLine2", division.AddressLine2);
                command.Parameters.AddWithValue(@"tAddressLine3", division.AddressLine3);
                command.Parameters.AddWithValue(@"tState", division.State);
                command.Parameters.AddWithValue(@"tCountry", division.Country);
                command.Parameters.AddWithValue(@"tZipCode", division.ZipCode);
                command.Parameters.AddWithValue(@"tPhoneNo", division.PhoneNo);
                command.Parameters.AddWithValue(@"tFaxNo", division.FaxNo);
                command.Parameters.AddWithValue(@"tUpdatedBy", division.UpdatedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Delete(int divisionId)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDivisionDelete", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tDivisionId", divisionId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<DivisionModel> GetDivisions()
        {
            DataTable table = new DataTable();
            List<DivisionModel> divisions = new List<DivisionModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                        SELECT 
	                        td.DivisionId,
	                        tc.CompanyId,
	                        tc.CompanyName,
	                        td.DivisionName,
	                        td.AddressLine1,
                            td.AddressLine2,
                            td.AddressLine3,
                            td.State,
                            td.Country,
                            td.ZipCode,
	                        td.PhoneNo,
	                        td.FaxNo,
	                        td.ObjectId,
	                        td.ObjectTypeCode,
	                        td.AddedBy,
	                        td.UpdatedBy

                        FROM 
	                        tDivision as td 
                        INNER JOIN
	                        tCompany as tc
                        ON
	                        td.ObjectId=tc.CompanyId
						AND
							td.ObjectTypeCode='CM'");

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    DivisionModel division = new DivisionModel
                    {
                        DivisionId = Convert.ToInt32(dr["DivisionId"]),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        AddressLine1 = dr["AddressLine1"].ToString().Trim(),
                        AddressLine2 = dr["AddressLine2"].ToString().Trim(),
                        AddressLine3 = dr["AddressLine3"].ToString().Trim(),
                        State = dr["State"].ToString().Trim(),
                        Country = dr["Country"].ToString().Trim(),
                        ZipCode = dr["ZipCode"].ToString().Trim(),
                        PhoneNo = dr["PhoneNo"].ToString().Trim(),
                        FaxNo = dr["FaxNo"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim()
                    };
                    divisions.Add(division);
                }
            }
            return divisions;
        }

        public static List<DivisionModel> GetDivisions(int objectId,string objectTypeCode)
        {
            DataTable table = new DataTable();
            List<DivisionModel> divisions = new List<DivisionModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                        SELECT 
	                        td.DivisionId,
	                        tc.CompanyId,
	                        tc.CompanyName,
	                        td.DivisionName,
	                        td.DivisionName,
	                        td.AddressLine1,
                            td.AddressLine2,
                            td.AddressLine3,
                            td.State,
                            td.Country,
                            td.ZipCode,
	                        td.PhoneNo,
	                        td.FaxNo,
	                        td.ObjectId,
	                        td.ObjectTypeCode,
	                        td.AddedBy,
	                        td.UpdatedBy

                        FROM 
	                        tDivision as td 
                        INNER JOIN
	                        tCompany as tc
                        ON
	                        td.ObjectId={0}
						AND
							td.ObjectTypeCode='{2}'", objectId,objectTypeCode);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    DivisionModel division = new DivisionModel
                    {
                        DivisionId = Convert.ToInt32(dr["DivisionId"]),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        AddressLine1 = dr["AddressLine1"].ToString().Trim(),
                        AddressLine2 = dr["AddressLine2"].ToString().Trim(),
                        AddressLine3 = dr["AddressLine3"].ToString().Trim(),
                        State = dr["State"].ToString().Trim(),
                        Country = dr["Country"].ToString().Trim(),
                        ZipCode = dr["ZipCode"].ToString().Trim(),
                        PhoneNo = dr["PhoneNo"].ToString().Trim(),
                        FaxNo = dr["FaxNo"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim()
                    };
                    divisions.Add(division);
                }
            }
            return divisions;
        }
    }
}