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

        public static void Delete(DepartmentUserModel departmentUser)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDepartmentUserDelete", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tUserId", departmentUser.UserId);

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
    }
}