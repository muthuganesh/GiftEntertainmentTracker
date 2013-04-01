using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RSQ.GiftEntertainmentTracker.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace RSQ.GiftEntertainmentTracker.DataAccess
{
    public class RolePremissionSetDAL
    {
        public static RolePermissionSetModel Get(string roleName)
        {
            DataRow dr;
            DataTable dt = new DataTable();
            RolePermissionSetModel rolePermissionSet = null;

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                string command = string.Format(@"
                SELECT 
                  * 
                FROM
                    tRolePermissionSet
                WHERE 
                    RoleName='{0}'", roleName);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                adapter.Fill(dt);
                connection.Close();

                if (dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    rolePermissionSet = new RolePermissionSetModel
                    {
                        RoleName = dr["RoleName"].ToString().Trim(),
                        PermissionSet = Convert.ToInt32(dr["PermissionSet"]),
                        PermissionSetType = dr["PermissionSetType"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]),
                    };
                }
            }
            return rolePermissionSet;
        }

        public static void Insert(RolePermissionSetModel rolePermissionSet)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tRolePermissionSetInsert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("tRoleName", rolePermissionSet.RoleName);
                command.Parameters.AddWithValue("tPermissionSet", rolePermissionSet.PermissionSet);
                command.Parameters.AddWithValue("tPermissionSetType", rolePermissionSet.PermissionSetType);
                command.Parameters.AddWithValue("tAddedBy", rolePermissionSet.AddedBy);

                //MySqlParameter parameter = new MySqlParameter("tAddedDate", SqlDbType.DateTime);
                //parameter.Direction = ParameterDirection.Output;
                //parameter.Value = DateTime.Now;
                //command.Parameters.Add(parameter);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                //rolePermissionSet.AddedDate = rolePermissionSet.UpdatedDate = Convert.ToDateTime(parameter.Value);
            }
        }

        public static void Update(RolePermissionSetModel rolePermissionSet)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tRolePermissionSetUpdate", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("tRoleName", rolePermissionSet.RoleName);
                command.Parameters.AddWithValue("tPermissionSet", rolePermissionSet.PermissionSet);
                command.Parameters.AddWithValue("tPermissionSetType", rolePermissionSet.PermissionSetType);
                command.Parameters.AddWithValue("tUpdatedBy", rolePermissionSet.UpdatedBy);

                //MySqlParameter parameter = new MySqlParameter("tLastUpdatedOn", SqlDbType.DateTime);
                //parameter.Direction = ParameterDirection.InputOutput;
                //command.Parameters.Add(parameter);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                //rolePermissionSet.UpdatedDate = Convert.ToDateTime(parameter.Value);
            }
        }

        public static void Delete(string roleName)
        {
            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                MySqlCommand command = new MySqlCommand("SP_tRolePermissionSetDelete", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("tRoleName", roleName);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<RolePermissionSetModel> GetRolePermissionSets(string roleName)
        {
            RolePermissionSetModel rolePermissionSet = null;
            List<RolePermissionSetModel> rolePermissionSets = new List<RolePermissionSetModel>();

            using (MySqlConnection connection = new MySqlConnection(Common.ConnectionString.GiftDb))
            {
                DataTable dt = new DataTable();
                string command = string.Format(@"
                SELECT 
                  * 
                FROM
                    tRolePermissionSet
                WHERE 
                    RoleName='{0}'", roleName);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, connection);
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    rolePermissionSet = new RolePermissionSetModel
                    {
                        RoleName = dr["RoleName"].ToString().Trim(),
                        PermissionSet = Convert.ToInt32(dr["PermissionSet"]),
                        PermissionSetType = dr["PermissionSetType"].ToString().Trim(),
                        AddedBy = dr["AddedBy"].ToString().Trim(),
                        AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                        UpdatedBy = dr["UpdatedBy"].ToString().Trim(),
                        UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]),
                    };
                    rolePermissionSets.Add(rolePermissionSet);
                }
            }
            return rolePermissionSets;
        }
    }
}