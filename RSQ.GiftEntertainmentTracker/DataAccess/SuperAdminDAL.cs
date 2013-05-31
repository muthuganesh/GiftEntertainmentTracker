using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace RSQ.GiftEntertainmentTracker.DataAccess
{
    public class SuperAdminDAL
    {
        public static List<UserModel> GetCompanyProfiles(int companyId)
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
                                WHERE
                                    tc.CompanyId={0}

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
                                    tc.CompanyId={0}",companyId);


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