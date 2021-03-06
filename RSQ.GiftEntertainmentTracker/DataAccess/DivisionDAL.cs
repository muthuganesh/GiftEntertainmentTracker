﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

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

        public static List<DivisionModel> GetDivisions(int? objectId, string objectTypeCode)
        {
            DataTable table = new DataTable();
            List<DivisionModel> divisions = new List<DivisionModel>();
            StringBuilder codeBuilder=new StringBuilder();
            string sAnd=" And ";
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                        (SELECT
                            *
                        FROM
                        (
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
                            td.AddedDate,
	                        td.UpdatedBy

                        FROM 
	                        tDivision as td 
                        INNER JOIN
	                        tCompany as tc
                        ON
	                        td.ObjectId=tc.CompanyId
						AND
							td.ObjectTypeCode='CM') X
                        WHERE <<<includeSql>>>) ORDER BY AddedDate DESC");
                        
                if(objectId.HasValue)
                {
                    codeBuilder.AppendFormat("ObjectId={0}",objectId);
                    codeBuilder.AppendFormat(sAnd);
                }
                if(!string.IsNullOrEmpty(objectTypeCode))
                {
                    codeBuilder.AppendFormat("ObjectTypeCode='{0}'",objectTypeCode);
                    codeBuilder.AppendFormat(sAnd);
                }

                codeBuilder.AppendFormat("AddedBy='{0}'",HttpContext.Current.User.Identity.Name);

                if(!string.IsNullOrEmpty(codeBuilder.ToString().Trim()))
                {
                    query=query.Replace("<<<includeSql>>>",codeBuilder.ToString().Trim());
                }
                else
                    query=query.Replace("WHERE <<<includeSql>>>",codeBuilder.ToString().Trim());

                query=query.ToString();

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
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim()
                    };
                    divisions.Add(division);
                }
            }
            return divisions;
        }

//        public static List<DivisionModel> GetDivisions(int objectId,string objectTypeCode)
//        {
//            DataTable table = new DataTable();
//            List<DivisionModel> divisions = new List<DivisionModel>();

//            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
//            {
//                string query = string.Format(@"
//                        SELECT 
//	                       *
//                        FROM 
//	                        tDivision as td 
//                        WHERE
//	                        td.ObjectId={0}
//						AND
//							td.ObjectTypeCode='{1}'", objectId,objectTypeCode);

//                MySqlCommand command = new MySqlCommand(query, connection);

//                connection.Open();
//                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
//                adapter.Fill(table);
//                connection.Close();

//                foreach (DataRow dr in table.Rows)
//                {
//                    DivisionModel division = new DivisionModel
//                    {
//                        DivisionId = Convert.ToInt32(dr["DivisionId"]),
//                        DivisionName=dr["DivisionName"].ToString().Trim(),
//                        AddressLine1 = dr["AddressLine1"].ToString().Trim(),
//                        AddressLine2 = dr["AddressLine2"].ToString().Trim(),
//                        AddressLine3 = dr["AddressLine3"].ToString().Trim(),
//                        State = dr["State"].ToString().Trim(),
//                        Country = dr["Country"].ToString().Trim(),
//                        ZipCode = dr["ZipCode"].ToString().Trim(),
//                        PhoneNo = dr["PhoneNo"].ToString().Trim(),
//                        FaxNo = dr["FaxNo"].ToString().Trim(),
//                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
//                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
//                        AddedBy = dr["AddedBy"].ToString().Trim(),
//                        UpdatedBy = dr["UpdatedBy"].ToString().Trim()
//                    };
//                    divisions.Add(division);
//                }
//            }
//            return divisions;
//        }
    }
}