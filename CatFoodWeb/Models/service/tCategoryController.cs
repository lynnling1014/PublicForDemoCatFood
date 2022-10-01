using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatFoodWeb.Models
{
    public class tCategoryController : Controller
    {
        //連線字串
        string strConn = ConfigurationManager.ConnectionStrings["dbCatFood2"].ConnectionString;
        SqlCommand cmd = new SqlCommand();

        //====================Category Read=================================
        //read table
        public classCategory[] readBySQLforCategoey(SqlCommand cmd)
        {
            SqlConnection conn = new SqlConnection(strConn);
            cmd.Connection = conn;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            //list to store data
            List<classCategory> categoriesList = new List<classCategory>();
            while (dr.Read())
            {
                categoriesList.Add(new classCategory()
                {
                    fCategoryID = (int)dr["fCategoryID"],
                    fCategoryName = dr["fCategoryName"].ToString(),
                    fCategoryDescription = dr["fCategoryDescription"].ToString(),
                    fSuggestOrderQty = (int)dr["fSuggestOrderQty"],
                    fSuggestMakeQty = (int)dr["fSuggestMakeQty"],
                    fTotal = (int)dr["fTotal"]
                });
            }
            cmd.Dispose();
            conn.Close();

            return categoriesList.ToArray();
        }
        //get all categoryName
        public classCategory[] getCategories()
        {
            string strSQL = @"select * from tCategory";
            cmd.CommandText = strSQL;
            return readBySQLforCategoey(cmd);
        }
    }
}