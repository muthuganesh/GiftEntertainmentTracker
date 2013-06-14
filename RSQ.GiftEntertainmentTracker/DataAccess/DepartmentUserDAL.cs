using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace RSQ.GiftEntertainmentTracker.DataAccess
{
    public class DepartmentUserDAL
    {
//        public static DepartmentUserModel Get(int userId)
//        {
//            DataTable table = new DataTable();
//            DepartmentUserModel departmentUser = new DepartmentUserModel();

//            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
//            {
//                string query = string.Format(@"
//                                SELECT
//					    u.UserId,
//                        tc.CompanyName,
//                        td.DivisionName,
//                        tdt.DepartmentName,
//                        u.UserName,
//						u.ObjectId AS ObjectId,
//                        u.ObjectTypeCode AS ObjectTypeCode,
//                        tdt.ObjectId,
//                        tdt.ObjectTypeCode,
//                        td.ObjectId,
//                        td.ObjectTypeCode,
//                        u.AddedBy,
//                        u.AddedDate,
//                        u.UpdatedBy,
//                        u.UpdatedDate
//                    FROM
//						tDepartmentUser as u
//					INNER JOIN
//                        tDepartment as tdt
//					ON
//						u.ObjectId=tdt.DepartmentId
//					AND
//						U.ObjectTypeCode='DP'
//                    INNER JOIN
//                        tDivision as td
//                    ON
//                        tdt.ObjectId=td.DivisionId
//                    AND
//                        tdt.ObjectTypeCode='DV'
//                    INNER JOIN
//                        tCompany as tc
//                    ON
//                        td.ObjectId=tc.CompanyId
//                    AND
//                        td.ObjectTypeCode='CM'
//                    WHERE
//                        u.UserId={0}
//                    UNION ALL
//
//                    SELECT
//                        u.UserId,
//                        u.UserName,
//                        tc.CompanyName,
//                        td.DivisionName,
//                        tdt.DepartmentName,
//						u.ObjectId AS ObjectId,
//                        u.ObjectTypeCode AS ObjectTypeCode,
//                        tdt.ObjectId,
//                        tdt.ObjectTypeCode,
//                        td.ObjectId,
//                        td.ObjectTypeCode,
//                        u.AddedBy,
//                        u.AddedDate,
//                        u.UpdatedBy,
//                        u.UpdatedDate
//					FROM
//						tDepartmentUser as u
//					INNER JOIN
//                        tDepartment as tdt
//					ON
//						u.ObjectId=tdt.DepartmentId
//					AND
//						U.ObjectTypeCode='DP'
//                    INNER JOIN
//                        tCompany as tc
//                    ON
//                        tdt.ObjectId=tc.CompanyId
//                    AND
//                        tdt.ObjectTypeCode='CM'
//                    LEFT JOIN
//                        tDivision as td
//                    ON
//                        tdt.ObjectId=td.DivisionId
//                    AND
//                        tdt.ObjectTypeCode='DV'
//                    WHERE
//                        u.UserId={0}",userId);

//                MySqlCommand command = new MySqlCommand(query, connection);

//                connection.Open();
//                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
//                adapter.Fill(table);
//                connection.Close();

//                foreach (DataRow dr in table.Rows)
//                {
//                    departmentUser.UserId = Convert.ToInt32(dr["UserId"]);
//                    departmentUser.UserName = dr["UserName"].ToString().Trim();
//                    departmentUser.CompanyName = dr["CompanyName"].ToString().Trim();
//                    departmentUser.DivisionName = dr["DivisionName"].ToString().Trim();
//                    departmentUser.DepartmentName = dr["DepartmentName"].ToString().Trim();
//                    departmentUser.ObjectId = Convert.ToInt32(dr["ObjectId"]);
//                    departmentUser.ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim();
//                    departmentUser.AddedBy = dr["AddedBy"].ToString().Trim();
//                    departmentUser.AddedDate = Convert.ToDateTime(dr["AddedDate"]);
//                    departmentUser.UpdatedBy = dr["UpdatedBy"].ToString().Trim();
//                    departmentUser.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
//                }
//            }
//            return departmentUser;
//        }

        public static void Insert(DepartmentUserModel departmentUser)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command=new MySqlCommand("SP_tDepartmentUserInsert",connection);
                command.CommandType=CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tUserName", departmentUser.UserName);
                command.Parameters.AddWithValue(@"tObjectId",departmentUser.ObjectId);
                command.Parameters.AddWithValue(@"tObjectTypeCode",departmentUser.ObjectTypeCode);
                command.Parameters.AddWithValue(@"tAddedBy",departmentUser.AddedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update(DepartmentUserModel departmentUser)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDepartmentUserUpdate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tUserId", departmentUser.UserId);
                command.Parameters.AddWithValue(@"tUserName", departmentUser.UserName);
                command.Parameters.AddWithValue(@"tObjectId", departmentUser.ObjectId);
                command.Parameters.AddWithValue(@"tObjectTypeCode", departmentUser.ObjectTypeCode);
                command.Parameters.AddWithValue(@"tUpdatedBy", departmentUser.UpdatedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Delete(int departmentUserId)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDepartmentUserDelete", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tUserId", departmentUserId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<DepartmentUserModel> GetDepartmentUsers()
        {
            DataTable table = new DataTable();
            List<DepartmentUserModel> departmentUsers = new List<DepartmentUserModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                                SELECT
					    u.UserId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        u.UserName,
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
						tDepartmentUser as u
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
                        u.UserName,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
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
						tDepartmentUser as u
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
                    DepartmentUserModel departmentUser = new DepartmentUserModel
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName=dr["UserName"].ToString().Trim(),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        DepartmentName = dr["DepartmentName"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"])
                    };
                    departmentUsers.Add(departmentUser);
                }
            }
            return departmentUsers;
        }

        public static List<DepartmentUserModel> GetDepartmentUsers(int objectId,string objectTypeCode)
        {
            DataTable table = new DataTable();
            List<DepartmentUserModel> departmentUsers = new List<DepartmentUserModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                                SELECT
					    u.UserId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        u.UserName,
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
						tDepartmentUser as u
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
                        u.ObjectId={0}
                    AND
                        u.ObjectTypeCode='{1}'
                    UNION ALL

                    SELECT
                        u.UserId,
                        u.UserName,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
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
						tDepartmentUser as u
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
                    DepartmentUserModel departmentUser = new DepartmentUserModel
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName = dr["UserName"].ToString().Trim(),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        DepartmentName = dr["DepartmentName"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"])
                    };
                    departmentUsers.Add(departmentUser);
                }
            }
            return departmentUsers;
        }
    }
}