using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatFoodWeb.Models
{
    public class tProductController : Controller
    {
        #region 取得所有商品getProducts
        public classProduct[] getProducts()
        {
            List<classProduct> productList = new List<classProduct>();

            string strSQL = @"select * from tProduct as p 
                                join tCategory as c on p.fCategoryID = c.fCategoryID
                                join tSupplier as s on p.fSupplierID = s.fSupplierID";
            DataTable dt = PF.readBySQL(strSQL);
            foreach (DataRow rs in dt.Rows)
            {
                productList.Add(new classProduct()
                {
                    fProductID = (int)rs["fProductID"],
                    fProductName = rs["fProductName"].ToString(),
                    fProductDescription = rs["fProductDescription"].ToString(),
                    fUnitPrice = (int)rs["fUnitPrice"],
                    fNewPrice = (int)rs["fNewPrice"],
                    fQtyPerUnit = (int)rs["fQtyPerUnit"],
                    fInStore = (int)rs["fInStore"],
                    fActive = (bool)rs["fActive"] == true ? "上架" : "下架",
                    fCategoryName = rs["fCategoryName"].ToString(),
                    fSupplierName = rs["fSupplierName"].ToString(),
                    fCreateDate = rs["fCreateDate"].ToString() == null ? "" : rs["fCreateDate"].ToString(),
                    fUpdateDate = rs["fUpdateDate"].ToString() == null ? "" : rs["fUpdateDate"].ToString(),
                    fPhoto = rs["fPhoto"].ToString() == null ? "" : rs["fPhoto"].ToString(),
                    fCategoryID = (int)rs["fCategoryID"],
                    fSupplierID = (int)rs["fSupplierID"]
                });
                
            }
                        
            return productList.ToArray();

        }
        #endregion

        #region 取得一件商品By ID
        public classProduct getProductByID(int id)
        {
            classProduct product = new classProduct();
           SqlParameter[] para  = { new SqlParameter("@fProductID", id) };
                string strSQL = @"select * from tProduct as p 
                                join tCategory as c on p.fCategoryID = c.fCategoryID
                                join tSupplier as s on p.fSupplierID = s.fSupplierID
                                where fProductID=@fProductID";
            DataTable dt = PF.readBySQL(strSQL, para);
            //一筆
            if (dt.Rows.Count >= 1)
            {
                DataRow rs = dt.Rows[0];
                product.fProductID = (int)rs["fProductID"];
                product.fProductName = rs["fProductName"].ToString();
                product.fProductDescription = rs["fProductDescription"].ToString();
                product.fUnitPrice = (int)rs["fUnitPrice"];
                product.fNewPrice = (int)rs["fNewPrice"];
                product.fQtyPerUnit = (int)rs["fQtyPerUnit"];
                product.fInStore = (int)rs["fInStore"];
                product.fActive = (bool)rs["fActive"] == true ? "上架" : "下架";
                product.fCategoryName = rs["fCategoryName"].ToString();
                product.fSupplierName = rs["fSupplierName"].ToString();
                product.fCreateDate = rs["fCreateDate"].ToString() == null ? "" : rs["fCreateDate"].ToString();
                product.fUpdateDate = rs["fUpdateDate"].ToString() == null ? "" : rs["fUpdateDate"].ToString();
                product.fPhoto = rs["fPhoto"].ToString() == null ? "" : rs["fPhoto"].ToString();
                product.fCategoryID = (int)rs["fCategoryID"];
                product.fSupplierID = (int)rs["fSupplierID"];
            }
            return product;
        }
        #endregion

        #region 新增商品
        public void insert(classProduct product)
        {
            string strSQL = @"insert into tProduct (fProductName , fProductDescription, fCategoryID,fSupplierID
                                , fUnitPrice, fNewPrice , fQtyPerUnit , fInStore, fPhoto , fCreateDate,fActive) 
                                values (@fProductName , @fProductDescription, @fCategoryID,@fSupplierID
                                , @fUnitPrice, @fNewPrice , @fQtyPerUnit , @fInStore, @fPhoto , @fCreateDate ,@fActive)";
            
            SqlParameter[] para = {
                new SqlParameter("@fProductName", product.fProductName),
                new SqlParameter("@fProductDescription", product.fProductDescription),
                new SqlParameter("@fCategoryID", product.fCategoryID),
                new SqlParameter("@fSupplierID", product.fSupplierID),
                new SqlParameter("@fUnitPrice", product.fUnitPrice),
                new SqlParameter("@fQtyPerUnit", product.fQtyPerUnit),
                new SqlParameter("@fNewPrice", product.fNewPrice),
                new SqlParameter("@fInStore", product.fInStore),
                new SqlParameter("@fPhoto", product.fPhoto==null?null:product.fPhoto),
                new SqlParameter("@fActive", product.fActive),
                new SqlParameter("@fCreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };
            PF.executeSQL(strSQL , para);
        }
        #endregion

        #region 修改商品
        public void update(classProduct product)
        {
            string strSQL = @"update tProduct set fProductName=@fProductName , fProductDescription=@fProductDescription, 
                                fCategoryID=@fCategoryID, fSupplierID=@fSupplierID, fUnitPrice = @fUnitPrice,
                                fNewPrice = @fNewPrice , fQtyPerUnit =@fQtyPerUnit , fInStore=@fInStore, 
                                fPhoto=@fPhoto , fUpdateDate=@fUpdateDate,fActive=@fActive where fProductID = @fProductID";
            SqlParameter[] para = {
                new SqlParameter("@fProductName", product.fProductName),
                new SqlParameter("@fProductDescription", product.fProductDescription),
                new SqlParameter("@fCategoryID", product.fCategoryID),
                new SqlParameter("@fSupplierID", product.fSupplierID),
                new SqlParameter("@fUnitPrice", product.fUnitPrice),
                new SqlParameter("@fQtyPerUnit", product.fQtyPerUnit),
                new SqlParameter("@fNewPrice", product.fNewPrice),
                new SqlParameter("@fInStore", product.fInStore),
                new SqlParameter("@fPhoto", product.fPhoto==null?null:product.fPhoto),
                new SqlParameter("@fActive", product.fActive),
                new SqlParameter("@fCreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@fUpdateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };
            PF.executeSQL(strSQL, para);
        }
        #endregion
    }
}