using Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository
    {
        System.Data.SqlClient.SqlConnection _connection;
        public Repository()
        {
            Init();
        }
        private void Init()
        {
            _connection = new System.Data.SqlClient.SqlConnection(GetConnectionString());
        }
        private void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
            _connection.Open();
        }
        private void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                this._connection.Close();
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public System.Data.DataSet ExecuteDataset(string sql)
        {
            var ds = new System.Data.DataSet();
            var cmd = new System.Data.SqlClient.SqlCommand(sql, _connection);
            cmd.CommandTimeout = 700;
            System.Data.SqlClient.SqlDataAdapter da;

            try
            {
                OpenConnection();
                //da = new SqlDataAdapter(sql, _connection);
                da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                CloseConnection();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                da = null;
                CloseConnection();
            }
            return ds;
        }

        public System.Data.DataRow ExecuteDataRow(string sql)
        {
            using (var ds = ExecuteDataset(sql))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;

                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                return ds.Tables[0].Rows[0];
            }
        }
        public System.Data.DataTable ExecuteDataTable(string sql)
        {
            using (var ds = ExecuteDataset(sql))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;

                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                return ds.Tables[0];
            }
        }
        public System.Collections.Generic.List<object> DropdownList(string sql)
        {
            var dt = ExecuteDataTable(sql);
            var list = new System.Collections.Generic.List<object>();
            if (null != dt)
            {
                foreach (System.Data.DataRow item in dt.Rows)
                {
                    list.Add(new { id = item["Value"], text = item["Name"].ToString() });
                }
            }
            return list;
        }


        public String FilterString(string strVal)
        {
            var str = FilterQuote(strVal);

            if (str.ToLower() != "null")
                str = "'" + str + "'";

            return str.TrimEnd().TrimStart();
        }

        public String FilterString_NVARCHAR(string strVal)
        {
            var str = FilterQuote(strVal);

            if (str.ToLower() != "null")
                str = "N'" + str + "'";

            return str.TrimEnd().TrimStart();
        }

        public String FilterStringDR(string strVal)
        {
            var str = FilterQuoteDR(strVal);

            if (str.ToLower() != "null")
                str = "'" + str + "'";

            return str.TrimEnd().TrimStart();
        }
        public string DBNullToValue(object obj)
        {
            if (obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return null;
        }
        public String FilterQuote(string strVal)
        {
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = "";
            }
            var str = strVal.Trim();

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                //str = str.Replace(",", "");
                str = str.Replace("--", "");
                str = str.Replace("'", "");

                str = str.Replace("/*", "");
                str = str.Replace("*/", "");

                str = str.Replace(" select ", "");
                str = str.Replace(" insert ", "");
                str = str.Replace(" update ", "");
                str = str.Replace(" delete ", "");

                str = str.Replace(" drop ", "");
                str = str.Replace(" truncate ", "");
                str = str.Replace(" create ", "");

                str = str.Replace(" begin ", "");
                str = str.Replace(" end ", "");
                str = str.Replace(" char(", "");

                str = str.Replace(" exec ", "");
                str = str.Replace(" xp_cmd ", "");


                str = str.Replace("<script", "");

            }
            else
            {
                str = "null";
            }
            return str;
        }
        public String FilterQuoteDR(string strVal)
        {
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = "";
            }
            var str = strVal.Trim();

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                //str = str.Replace(",", "");
                str = str.Replace("--", "");
                str = str.Replace("'", "''");

                str = str.Replace("/*", "");
                str = str.Replace("*/", "");

                str = str.Replace(" select ", "");
                str = str.Replace(" insert ", "");
                str = str.Replace(" update ", "");
                str = str.Replace(" delete ", "");

                str = str.Replace(" drop ", "");
                str = str.Replace(" truncate ", "");
                str = str.Replace(" create ", "");

                str = str.Replace(" begin ", "");
                str = str.Replace(" end ", "");
                str = str.Replace(" char(", "");

                str = str.Replace(" exec ", "");
                str = str.Replace(" xp_cmd ", "");


                str = str.Replace("<script", "");

            }
            else
            {
                str = "null";
            }
            return str;
        }
        public DbResponse ParseDbResponse(System.Data.DataTable dt)
        {

            var res = new DbResponse();
            if (null == dt)
            {
                res.ErrorCode = 0;
                res.Message = "";
                res.Id = "";
                return res;
            }
            if (dt.Rows.Count > 0)
            {
                res.ErrorCode = Convert.ToInt32(dt.Rows[0][0].ToString());
                res.Message = dt.Rows[0][1].ToString();
                res.Id = dt.Rows[0][2].ToString();
                if (dt.Columns.Count > 3 && dt.Columns.Count < 5)
                {
                    res.Extra = dt.Rows[0][3].ToString();
                }
                if (dt.Columns.Count > 4)
                {
                    res.Extra = dt.Rows[0][3].ToString();
                    res.Extra2 = dt.Rows[0][4].ToString();
                }
            }
            return res;
        }

        public DbResponse ParseDbResponse(string sql)
        {
            return ParseDbResponse(ExecuteDataTable(sql));
        }
        //public static class ConnectionSettings
        //{
        //    public static AppSettingsReader aps = new AppSettingsReader();
        //    public static SqlConnection GetConnection()
        //    {
        //        SqlConnection con = new SqlConnection(aps.GetValue("DefaultConnection", typeof(string)).ToString());
        //        if (con.State != ConnectionState.Open)
        //        {
        //            con.Open();
        //        }
        //        return con;

        //    }
        //}
    }
}
