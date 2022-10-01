using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public partial class PF
{
    public static SqlConnection dbOpen()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbCatFood2"].ConnectionString;
            SqlConnection conn = null;
            conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
        return null;
    }

    //execute sql to CUD 
    public static SqlConnection executeSQL(string strSQL, SqlParameter[] i=null)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = PF.dbOpen())
            {
                cmd.Connection = conn;
                cmd.CommandText = strSQL;
                if (i != null)
                {
                    cmd.Parameters.AddRange(i);
                }
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            string errMsg =  ex.ToString();
            Console.WriteLine(errMsg);
        }
        return null;
    }

    #region readBySQL
    public static DataTable readBySQL(string strsql, SqlParameter[] i = null)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = PF.dbOpen())
            {
                cmd.Connection = conn;
                cmd.CommandText = strsql;
                if (i != null)
                {
                    cmd.Parameters.AddRange(i);
                }


                SqlDataReader dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }

        return dt;
    }
    #endregion
}
