using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatFoodWeb.Models
{
    public class DBController : Controller
    {
        //連線字串
        string strConn = ConfigurationManager.ConnectionStrings["dbCatFood2"].ConnectionString;
        SqlCommand cmd = new SqlCommand();

        //execute sql to CUD 
        private SqlConnection executeSQL()
        {
            SqlConnection conn = new SqlConnection(strConn);
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return conn;
        }

        
    }
}