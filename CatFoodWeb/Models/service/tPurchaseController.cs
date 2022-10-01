using CatFoodWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatFoodWeb.Models.service
{
    public class tPurchaseController : Controller
    {
        #region 取得所有訂單getOrders
        public classOrder[] getOrders(int? type)
        {
            List<classOrder> orderList = new List<classOrder>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable(); 
            if (type != null)
            {
                string status = (int)type == 0 ? "In" : "Out";
                strSQL = @"select * from tOrder where fInOrOut=@status";
                SqlParameter[] para = {
                new SqlParameter("@status", status)
                 };
                dt = PF.readBySQL(strSQL, para);

            }
            else
            {
                strSQL = @"select * from tOrder";
                dt = PF.readBySQL(strSQL);
            }
            
            foreach (DataRow rs in dt.Rows)
            {
                orderList.Add(new classOrder()
                {
                    fOrderID = (int)rs["fOrderID"],
                    fArriveDate = rs["fArriveDate"].ToString() == null ? "" : rs["fArriveDate"].ToString(),
                    fInOrOut = rs["fInOrOut"].ToString(),
                    fCount = Convert.ToDecimal(rs["fCount"]),
                    fTransferFee = Convert.ToDecimal(rs["fTransferFee"]),
                    fTotalPrice = Convert.ToDecimal(rs["fTotalPrice"]),
                    fCreateDate = rs["fCreateDate"].ToString() == null ? "" : rs["fCreateDate"].ToString(),
                    fUpdateDate = rs["fUpdateDate"].ToString() == null ? "" : rs["fUpdateDate"].ToString(),
                    fArrive = rs["fArrive"].ToString(),
                    fNote = rs["fNote"].ToString()
                });
            }
                return orderList.ToArray();
        }
        #endregion

        #region 取得一張訂單getOrder
        public PurchaseViewModel getOneOrder(int id)
        {
            PurchaseViewModel order = new PurchaseViewModel();
            List<OrderDetails> OrderDetailList = new List<OrderDetails>();
            string strSQL = @"select * from tOrder as o 
                                join tOrderDetail as od on o.fOrderID = od.fOrderID 
                                join tProduct as p on p.fProductID = od.fProductID
                                where o.fOrderID=@fOrderID";
            SqlParameter[] para = {
                new SqlParameter("@fOrderID", id)
            };
            DataTable dt = PF.readBySQL(strSQL,para);
            foreach (DataRow rs in dt.Rows)
            {
                OrderDetailList.Add(new OrderDetails()
                {
                    productID = Convert.ToInt32(rs["fProductID"]),
                    productName = rs["fProductName"].ToString(),
                    pQty = Convert.ToInt32(rs["fQty"]),
                    buyPrice = Convert.ToInt32(rs["fPrice"])
                });
                order.fOrderID = Convert.ToInt32(rs["fOrderID"]);
                order.fArrive = rs["fArrive"].ToString();
                order.fArriveDate = Convert.ToDateTime(rs["fArriveDate"]).ToString("yyyy-MM-dd");
                order.fInOrOut = rs["fInOrOut"].ToString();
                order.fCount = Convert.ToInt32(rs["fCount"]);
                order.fTransferFee = Convert.ToInt32(rs["fTransferFee"]);
                order.fTotalPrice = Convert.ToInt32(rs["fTotalPrice"]);
                order.fCreateDate = rs["fCreateDate"].ToString();
                order.fUpdateDate = rs["fUpdateDate"].ToString();
                order.fNote = rs["fNote"].ToString();
            }
            order.details = OrderDetailList;
            return order;
        }
        #endregion

        #region 新增訂單+訂單明細
        public void insert(PurchaseViewModel orders)
        {
            string strSQL = @"insert into tOrder (fArriveDate
                                ,fInOrOut
                                ,fCount
                                ,fTransferFee
                                ,fTotalPrice
                                ,fCreateDate
                                ,fUpdateDate
                                ,fArrive
                                ,fNote) values (@fArriveDate
                                ,@fInOrOut
                                ,@fCount
                                ,@fTransferFee
                                ,@fTotalPrice
                                ,@fCreateDate
                                ,@fUpdateDate
                                ,@fArrive
                                ,@fNote);
                                ";
            string strSQL1 = @"SELECT top 1 fOrderID  from tOrder order by fOrderID desc;";
            string strSQL2 = @"Insert into tOrderDetail (fOrderID
                                ,fProductID
                                ,fPrice
                                ,fQty) values(@fOrderID
                                ,@fProductID
                                ,@fPrice
                                ,@fQty);";
            string strSQL3 = @"update tProduct set fInStore= fInStore + @fInStore where fProductID=@fProductID;";

            try
            {
                SqlParameter[] para = {
                new SqlParameter("@fArriveDate", orders.fArriveDate),
                new SqlParameter("@fInOrOut", orders.fInOrOut),
                new SqlParameter("@fCount", orders.fCount),
                new SqlParameter("@fTransferFee", orders.fTransferFee),
                new SqlParameter("@fTotalPrice", orders.fTotalPrice),
                new SqlParameter("@fCreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@fUpdateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@fArrive", orders.fArrive),
                new SqlParameter("@fNote", orders.fNote)
                };

                PF.executeSQL(strSQL, para);

                int ID = 0;
                DataTable dt = PF.readBySQL(strSQL1);
                foreach (DataRow rs in dt.Rows)
                {
                    ID = Convert.ToInt32(rs["fOrderID"]);
                }
                foreach (var item in orders.details)
                {
                    SqlParameter[] para2 = {
                        new SqlParameter("@fOrderID", ID),
                        new SqlParameter("@fProductID", item.productID),
                        new SqlParameter("@fPrice", item.buyPrice),
                        new SqlParameter("@fQty", item.pQty)
                    };
                    PF.executeSQL(strSQL2, para2);
                    int fInStore = item.pWeight;
                    SqlParameter[] para3 = {
                        new SqlParameter("@fInStore", fInStore),
                        new SqlParameter("@fProductID", item.productID)
                    };
                    PF.executeSQL(strSQL3, para3);
                    countStorageWeight();//計算類別重量寫入category table
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
        }
        #endregion

        #region 統計重量
        
        public void countStorageWeight()
        {
            string strSQL = @"update a set a.fTotal = b.total
                    from tCategory a,
                    (select fCategoryID, sum(fInStore) as total from tProduct group by fCategoryID) b 
                    where a.fCategoryID = b.fCategoryID";
            PF.executeSQL(strSQL);
        }

       #endregion
    }
}