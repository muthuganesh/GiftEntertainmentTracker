using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RSQ.GiftEntertainmentTracker.Models;
using MySql.Data.MySqlClient;

namespace RSQ.GiftEntertainmentTracker.DataAccess
{
    public class DepartmentDAL
    {
        public static DepartmentModel Get(int departmentId)
        {
            DataTable dt = new DataTable();
            DepartmentModel department = new DepartmentModel();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string command = string.Format(@"
                      SELECT
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                        tdt.DepartmentId={0}

                    UNION ALL

                    SELECT
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                        tdt.DepartmentId={0}", departmentId);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    department.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
                    department.DepartmentName = dr["DepartmentName"].ToString().Trim();
                    department.PhoneNo = dr["PhoneNo"].ToString().Trim();
                    department.FaxNo = dr["FaxNo"].ToString().Trim();
                    department.ObjectId = Convert.ToInt32(dr["ObjectId"]);
                    department.ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim();
                    department.AddedBy = dr["AddedBy"].ToString().Trim();
                    department.AddedDate = Convert.ToDateTime(dr["AddedDate"]);
                    department.UpdatedBy = dr["UpdatedBy"].ToString().Trim();
                    department.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                }
            }
            return department;
        }

        public static void Insert(DepartmentModel department)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDepartmentInsert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tDepartmentName", department.DepartmentName);
                command.Parameters.AddWithValue(@"tPhoneNo", department.PhoneNo);
                command.Parameters.AddWithValue(@"tFaxNo", department.FaxNo);
                command.Parameters.AddWithValue(@"tObjectId", department.ObjectId);
                command.Parameters.AddWithValue(@"tObjectTypeCode", department.ObjectTypeCode);
                command.Parameters.AddWithValue(@"tAddedBy", department.AddedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update(DepartmentModel division)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDepartmentUpdate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tDepartmentId", division.DepartmentId);
                command.Parameters.AddWithValue(@"tDepartmentName", division.DepartmentName);
                command.Parameters.AddWithValue(@"tPhoneNo", division.PhoneNo);
                command.Parameters.AddWithValue(@"tFaxNo", division.FaxNo);
                command.Parameters.AddWithValue(@"tUpdatedBy", division.UpdatedBy);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Delete(int departmentId)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tDepartmentDelete", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue(@"tDepartmentId", departmentId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<DepartmentModel> GetDepartments()
        {
            DataTable table = new DataTable();
            List<DepartmentModel> departments = new List<DepartmentModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                    SELECT
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                    DepartmentModel department = new DepartmentModel
                    {
                        DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                        CompanyName=dr["CompanyName"].ToString().Trim(),
                        DivisionName=dr["DivisionName"].ToString().Trim(),
                        DepartmentName = dr["DepartmentName"].ToString().Trim(),
                        PhoneNo = dr["PhoneNo"].ToString().Trim(),
                        FaxNo = dr["FaxNo"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"])
                    };
                    departments.Add(department);
                }
            }
            return departments;
        }

        public static List<DepartmentModel> GetDepartments(int objectId, string objectTypeCode)
        {
            DataTable table = new DataTable();
            List<DepartmentModel> departments = new List<DepartmentModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                    SELECT
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                        tdt.ObjectId={0}
                    AND
                        tdt.ObjectTypeCode='{1}'",objectId,objectTypeCode);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    DepartmentModel department = new DepartmentModel
                    {
                        DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        DepartmentName = dr["DepartmentName"].ToString().Trim(),
                        PhoneNo = dr["PhoneNo"].ToString().Trim(),
                        FaxNo = dr["FaxNo"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"])
                    };
                    departments.Add(department);
                }
            }
            return departments;
        }

        public static List<DepartmentModel> GetDepartments(int companyId)
        {
            DataTable table = new DataTable();
            List<DepartmentModel> departments = new List<DepartmentModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string query = string.Format(@"
                    SELECT
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                        tdt.DepartmentId AS DepartmentId,
                        td.DivisionId AS DivisionId,
                        tc.CompanyId AS CompanyId,
                        tc.CompanyName,
                        td.DivisionName,
                        tdt.DepartmentName,
                        tdt.PhoneNo,
                        tdt.FaxNo,
                        tdt.ObjectId AS ObjectId,
                        tdt.ObjectTypeCode AS ObjectTypeCode,
                        td.ObjectId,
                        td.ObjectTypeCode,
                        tdt.AddedBy,
                        tdt.AddedDate,
                        tdt.UpdatedBy,
                        tdt.UpdatedDate
                    FROM
                        tDepartment as tdt
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
                        tdt.ObjectId={0}", companyId);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                connection.Close();

                foreach (DataRow dr in table.Rows)
                {
                    DepartmentModel department = new DepartmentModel
                    {
                        DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                        CompanyName = dr["CompanyName"].ToString().Trim(),
                        DivisionName = dr["DivisionName"].ToString().Trim(),
                        DepartmentName = dr["DepartmentName"].ToString().Trim(),
                        PhoneNo = dr["PhoneNo"].ToString().Trim(),
                        FaxNo = dr["FaxNo"].ToString().Trim(),
                        ObjectId = Convert.ToInt32(dr["ObjectId"]),
                        ObjectTypeCode = dr["ObjectTypeCode"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"])
                    };
                    departments.Add(department);
                }
            }
            return departments;
        }
    }
}