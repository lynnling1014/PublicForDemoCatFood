using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatFoodWeb.Models.service
{
    public class tSupplierController : Controller
    {
        //連線字串
        string strConn = ConfigurationManager.ConnectionStrings["dbCatFood2"].ConnectionString;
        SqlCommand cmd = new SqlCommand();

        //====================Supplier Read=================================
        //read table
        public classSupplier[] readBySQLforSupplier(SqlCommand cmd)
        {
            SqlConnection conn = new SqlConnection(strConn);
            cmd.Connection = conn;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            //list to store data
            List<classSupplier> supplierList = new List<classSupplier>();
            while (dr.Read())
            {
                supplierList.Add(new classSupplier()
                {
                    fSupplierID = (int)dr["fSupplierID"],
                    fSupplierName = dr["fSupplierName"].ToString(),
                    fContactName = dr["fContactName"].ToString(),
                    fCompanyAddress = dr["fCompanyAddress"].ToString(),
                    fPhone = dr["fPhone"].ToString(),
                    fCompanyPhone = dr["fCompanyPhone"].ToString(),
                });
            }
            cmd.Dispose();
            conn.Close();

            return supplierList.ToArray();
        }
        //get all Supplier
        public classSupplier[] getSupplier()
        {
            string strSQL = @"select * from tSupplier";
            cmd.CommandText = strSQL;
            return readBySQLforSupplier(cmd);
        }
    }
}