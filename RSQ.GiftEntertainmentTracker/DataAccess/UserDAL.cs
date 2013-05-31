using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace RSQ.GiftEntertainmentTracker.DataAccess
{
    public class UserDAL
    {
        public static UserModel Get(int userId)
        {
            DataTable table = new DataTable();
            UserModel user = new UserModel();
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                                SELECT
					    u.UserId,
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
						u.FirstName,
						u.LastName,
                        u.AddressLine1,
                        u.AddressLine2,
                        u.State,
                        u.Country,
                        u.ZipCode,
                        u.EmailId,
                        u.PhoneNo,
						u.ObjectId AS ObjectId,
                        u.ObjectTypeCode AS ObjectTypeCode,
                        tdt.ObjectId,
                        tdt.ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        u.AddedBy,
                        u.AddedDate,
                        u.UpdatedBy,
                        u.UpdatedDate
                    FROM
						tUser as u
					INNER JOIN
                        tDepartment as tdt
					ON
						u.ObjectId=tdt.DepartmentId
					AND
						U.ObjectTypeCode='DP'
                    INNER JOIN
                        tDivision as td
                    ON
                        tdt.ObjectId=td.DivisionId
                    AND
                        tdt.ObjectTypeCode='DV'
                    INNER JOIN
                        tCompany as tc
                    ON
                        td.ObjectId=tc.CompanyId
                    AND
                        td.ObjectTypeCode='CM'
                    WHERE
                    u.UserId={0}

                    UNION ALL

                    SELECT
                        u.UserId,
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
						u.FirstName,
						u.LastName,
                        u.AddressLine1,
                        u.AddressLine2,
                        u.State,
                        u.Country,
                        u.ZipCode,
                        u.EmailId,
                        u.PhoneNo,
						u.ObjectId AS ObjectId,
                        u.ObjectTypeCode AS ObjectTypeCode,
                        tdt.ObjectId,
                        tdt.ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        u.AddedBy,
                        u.AddedDate,
                        u.UpdatedBy,
                        u.UpdatedDate
					FROM
						tUser as u
					INNER JOIN
                        tDepartment as tdt
					ON
						u.ObjectId=tdt.DepartmentId
					AND
						U.ObjectTypeCode='DP'
                    INNER JOIN
                        tCompany as tc
                    ON
                        tdt.ObjectId=tc.CompanyId
                    AND
                        tdt.ObjectTypeCode='CM'
                    LEFT JOIN
                        tDivision as td
                    ON
                        tdt.ObjectId=td.DivisionId
                    AND
                        tdt.ObjectTypeCode='DV'
                    WHERE
                        u.UserId={0}", userId);


                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                    user.CompanyName = dr["CompanyName"].ToString().Trim();
                    user.DivisionId = dr["DivisionId"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DivisionId"]);
                    user.DivisionName = dr["DivisionName"].ToString().Trim();
                    user.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
                    user.DepartmentName = dr["DepartmentName"].ToString().Trim();
                    user.FirstName = dr["FirstName"].ToString().Trim();
                    user.LastName = dr["LastName"].ToString().Trim();
                    user.AddressLine1 = dr["AddressLine1"].ToString().Trim();
                    user.AddressLine2 = dr["AddressLine2"].ToString().Trim();
                    user.State = dr["State"].ToString().Trim();
                    user.Country = dr["Country"].ToString().Trim();
                    user.ZipCode = dr["ZipCode"].ToString().Trim();
                    user.ObjectId = Convert.ToInt32(dr["ObjectId"]);
                    user.ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim();
                    user.EmailId = dr["EmailId"].ToString().Trim();
                    user.PhoneNo = dr["PhoneNo"].ToString().Trim();
                    user.AddedBy = dr["AddedBy"].ToString().Trim();
                    user.AddedDate = Convert.ToDateTime(dr["AddedDate"]);
                    user.UpdatedBy = dr["UpdatedBy"].ToString().Trim();
                    user.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                }
            }

            return user;
        }

        public static void Insert(UserModel user)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tUserInsert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("tFirstName", user.FirstName);
                command.Parameters.AddWithValue("tLastName", user.LastName);
                command.Parameters.AddWithValue("tAddressLine1", user.AddressLine1);
                command.Parameters.AddWithValue("tAddressLine2", user.AddressLine2);
                command.Parameters.AddWithValue("tState", user.State);
                command.Parameters.AddWithValue("tCountry", user.Country);
                command.Parameters.AddWithValue("tZipCode", user.ZipCode);
                command.Parameters.AddWithValue("tEmailId", user.EmailId);
                command.Parameters.AddWithValue("tPhoneNo", user.PhoneNo);
                command.Parameters.AddWithValue("tObjectId", user.ObjectId);
                command.Parameters.AddWithValue("tObjectTypeCode", user.ObjectTypeCode);
                command.Parameters.AddWithValue("tAddedBy", user.AddedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update(UserModel user)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tUserUpdate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("tUserId", user.UserId);
                command.Parameters.AddWithValue("tFirstName", user.FirstName);
                command.Parameters.AddWithValue("tLastName", user.LastName);
                command.Parameters.AddWithValue("tAddressLine1", user.AddressLine1);
                command.Parameters.AddWithValue("tAddressLine2", user.AddressLine2);
                command.Parameters.AddWithValue("tState", user.State);
                command.Parameters.AddWithValue("tCountry", user.Country);
                command.Parameters.AddWithValue("tZipCode", user.ZipCode);
                command.Parameters.AddWithValue("tEmailId", user.EmailId);
                command.Parameters.AddWithValue("tPhoneNo", user.PhoneNo);
                command.Parameters.AddWithValue("tUpdatedBy", user.UpdatedDate);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Delete(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tUserDelete", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("tUserId", userId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<UserModel> GetUsers()
        {
            DataTable table = new DataTable();
            List<UserModel> users = new List<UserModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                                SELECT
					    u.UserId,
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
						u.FirstName,
						u.LastName,
                        u.AddressLine1,
                        u.AddressLine2,
                        u.State,
                        u.Country,
                        u.ZipCode,
                        u.EmailId,
                        u.PhoneNo,
						u.ObjectId AS ObjectId,
                        u.ObjectTypeCode AS ObjectTypeCode,
                        tdt.ObjectId,
                        tdt.ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        u.AddedBy,
                        u.AddedDate,
                        u.UpdatedBy,
                        u.UpdatedDate
                    FROM
						tUser as u
					INNER JOIN
                        tDepartment as tdt
					ON
						u.ObjectId=tdt.DepartmentId
					AND
						U.ObjectTypeCode='DP'
                    INNER JOIN
                        tDivision as td
                    ON
                        tdt.ObjectId=td.DivisionId
                    AND
                        tdt.ObjectTypeCode='DV'
                    INNER JOIN
                        tCompany as tc
                    ON
                        td.ObjectId=tc.CompanyId
                    AND
                        td.ObjectTypeCode='CM'

                    UNION ALL

                    SELECT
                        u.UserId,
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
						u.FirstName,
						u.LastName,
                        u.AddressLine1,
                        u.AddressLine2,
                        u.State,
                        u.Country,
                        u.ZipCode,
                        u.EmailId,
                        u.PhoneNo,
						u.ObjectId AS ObjectId,
                        u.ObjectTypeCode AS ObjectTypeCode,
                        tdt.ObjectId,
                        tdt.ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        u.AddedBy,
                        u.AddedDate,
                        u.UpdatedBy,
                        u.UpdatedDate
					FROM
						tUser as u
					INNER JOIN
                        tDepartment as tdt
					ON
						u.ObjectId=tdt.DepartmentId
					AND
						U.ObjectTypeCode='DP'
                    INNER JOIN
                        tCompany as tc
                    ON
                        tdt.ObjectId=tc.CompanyId
                    AND
                        tdt.ObjectTypeCode='CM'
                    LEFT JOIN
                        tDivision as td
                    ON
                        tdt.ObjectId=td.DivisionId
                    AND
                        tdt.ObjectTypeCode='DV'");
               

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    UserModel user = new UserModel
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        CompanyId = Convert.ToInt32(dr["CompanyId"]),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionId = dr["DivisionId"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DivisionId"]),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                        DepartmentName = dr["DepartmentName"].ToString().Trim(),
                        FirstName = dr["FirstName"].ToString().Trim(),
                        LastName = dr["LastName"].ToString().Trim(),
                        AddressLine1 = dr["AddressLine1"].ToString().Trim(),
                        AddressLine2 = dr["AddressLine2"].ToString().Trim(),
                        State = dr["State"].ToString().Trim(),
                        Country = dr["Country"].ToString().Trim(),
                        ZipCode = dr["ZipCode"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        EmailId = dr["EmailId"].ToString().Trim(),
                        PhoneNo=dr["PhoneNo"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"])
                    };
                    users.Add(user);
                }
            }

            return users;
        }

        public static List<UserModel> GetUsers(int objectId,string objectTypeCode)
        {
            DataTable table = new DataTable();
            List<UserModel> users = new List<UserModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                                SELECT
					    u.UserId,
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
						u.FirstName,
						u.LastName,
                        u.AddressLine1,
                        u.AddressLine2,
                        u.State,
                        u.Country,
                        u.ZipCode,
                        u.EmailId,
                        u.PhoneNo,
						u.ObjectId AS ObjectId,
                        u.ObjectTypeCode AS ObjectTypeCode,
                        tdt.ObjectId,
                        tdt.ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        u.AddedBy,
                        u.AddedDate,
                        u.UpdatedBy,
                        u.UpdatedDate
                    FROM
						tUser as u
					INNER JOIN
                        tDepartment as tdt
					ON
						u.ObjectId=tdt.DepartmentId
					AND
						U.ObjectTypeCode='DP'
                    INNER JOIN
                        tDivision as td
                    ON
                        tdt.ObjectId=td.DivisionId
                    AND
                        tdt.ObjectTypeCode='DV'
                    INNER JOIN
                        tCompany as tc
                    ON
                        td.ObjectId=tc.CompanyId
                    AND
                        td.ObjectTypeCode='CM'

                    UNION ALL

                    SELECT
                        u.UserId,
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
						u.FirstName,
						u.LastName,
                        u.AddressLine1,
                        u.AddressLine2,
                        u.State,
                        u.Country,
                        u.ZipCode,
                        u.EmailId,
                        u.PhoneNo,
						u.ObjectId AS ObjectId,
                        u.ObjectTypeCode AS ObjectTypeCode,
                        tdt.ObjectId,
                        tdt.ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        u.AddedBy,
                        u.AddedDate,
                        u.UpdatedBy,
                        u.UpdatedDate
					FROM
						tUser as u
					INNER JOIN
                        tDepartment as tdt
					ON
						u.ObjectId=tdt.DepartmentId
					AND
						U.ObjectTypeCode='DP'
                    INNER JOIN
                        tCompany as tc
                    ON
                        tdt.ObjectId=tc.CompanyId
                    AND
                        tdt.ObjectTypeCode='CM'
                    LEFT JOIN
                        tDivision as td
                    ON
                        tdt.ObjectId=td.DivisionId
                    AND
                        tdt.ObjectTypeCode='DV'
                    WHERE
                        u.ObjectId={0}
                    AND
                        u.ObjectTypeCode='{1}'",objectId,objectTypeCode);


                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    UserModel user = new UserModel
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        CompanyId = Convert.ToInt32(dr["CompanyId"]),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionId = dr["DivisionId"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["DivisionId"]),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                        DepartmentName = dr["DepartmentName"].ToString().Trim(),
                        FirstName = dr["FirstName"].ToString().Trim(),
                        LastName = dr["LastName"].ToString().Trim(),
                        AddressLine1 = dr["AddressLine1"].ToString().Trim(),
                        AddressLine2 = dr["AddressLine2"].ToString().Trim(),
                        State = dr["State"].ToString().Trim(),
                        Country = dr["Country"].ToString().Trim(),
                        ZipCode = dr["ZipCode"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        EmailId = dr["EmailId"].ToString().Trim(),
                        PhoneNo = dr["PhoneNo"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"])
                    };
                    users.Add(user);
                }
            }

            return users;
        }
    }
}