using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CatFoodWeb.Models
{
    public class classOrder
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
    }
}