using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CatFoodWeb.Models.ViewModel
{
    public enum PurchaseType
    {
        In =0, //進貨
        Out =1 //出貨(製作使用)
    }
    //訂單(含多筆商品)
    public class PurchaseViewModel : IEnumerable<classProduct>, IEnumerable<OrderDetails>
    {
        [DisplayName("編號")]
        public int fOrderID { get; set; }

        [DisplayName("到貨日期")]
        public string fArriveDate { get; set; }

        [DisplayName("進/出貨")]
        public string fInOrOut { get; set; }

        [DisplayName("小計")]
        public decimal fCount { get; set; }

        [DisplayName("運費")]
        public decimal fTransferFee { get; set; }

        [DisplayName("總金額")]
        public decimal fTotalPrice { get; set; }

        [DisplayName("建立日期")]
        public string fCreateDate { get; set; }

        [DisplayName("更新日期")]
        public string fUpdateDate { get; set; }

        [DisplayName("貨運狀態")]
        public string fArrive { get; set; }

        [DisplayName("備註")]
        public string fNote { get; set; }

        //orderDetail

        [DisplayName("購買價格")]
        public string fPrice { get; set; }

        [DisplayName("數量")]
        public string fQty { get; set; }

        //商品資料

        public List<classProduct> product = new List<classProduct>();

        public IEnumerator<classProduct> GetEnumerator()
        {
            return ((IEnumerable<classProduct>)product).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)product).GetEnumerator();
        }

        

        //orderDetail
        public List<OrderDetails> details = new List<OrderDetails>();

        IEnumerator<OrderDetails> IEnumerable<OrderDetails>.GetEnumerator()
        {
            return ((IEnumerable<OrderDetails>)details).GetEnumerator();
        }
    }

    #region OrderDetails
    public class OrderDetails
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public int pQty { get; set; }
        public int buyPrice { get; set; }
        public int pWeight { get; set; }
    }
    #endregion

    #region 前端傳後端Model
    public class ViewDataModel
    {
        public int pID { get; set; }
        public int pQty { get; set; }
        public int pCategoryID { get; set; }
    }
    #endregion

    #region 前端傳後端Model
    public class ViewDataModel2
    {
        //ArriveDate TransferFee Note InOrOut
        public string ArriveDate { get; set; }
        public int TransferFee { get; set; }
        public string Note { get; set; }
        public string InOrOut { get; set; }
    }
    #endregion
}